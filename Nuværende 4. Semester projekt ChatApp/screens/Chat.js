// Import react modules
import React, { useState, useEffect, useCallback } from 'react';
import { TouchableOpacity, Text, View, StyleSheet, Image } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { GiftedChat, Send } from 'react-native-gifted-chat';

// Import Firebase modules
import { collection, addDoc, orderBy, query, onSnapshot, limit, getDocs, startAfter } from 'firebase/firestore';
import { getStorage, ref, uploadBytesResumable, getDownloadURL } from "firebase/storage";
import { db, auth } from '../firebase';

// Import components
import RenderSend from '../src/components/RenderSend';
import RenderBubble from '../src/components/RenderBubble';
import { timeFormat } from '../src/components/TimeFormat';
import * as ImagePicker from 'expo-image-picker';


// Create the Chat component
export default function Chat({ route }) {
    const [room, setRoom] = useState(route.params?.roomId);
    const [messages, setMessages] = useState([]);
    const [lastVisible, setLastVisible] = useState(null);
    const [loading, setLoading] = useState(false);
    const [loadEarlier, setLoadEarlier] = useState(true);
    const navigation = useNavigation();
    const storage = getStorage();

    // Fetch the chat messages
    useEffect(() => {
        const collectionRef = collection(db, 'chatrooms', room, 'messages');
        const q = query(collectionRef, orderBy('createdAt', 'desc'), limit(50));

        // Subscribe to the query and update the messages state
        const unsubscribe = onSnapshot(q, snapshot => {
            const lastVisible = snapshot.docs[snapshot.docs.length - 1];
            setLastVisible(lastVisible);
            setMessages(
                snapshot.docs.map(doc => ({
                    _id: doc.id,
                    text: doc.data().text,
                    createdAt: doc.data().createdAt.toDate(),
                    user: doc.data().user,
                    image: doc.data().image,
                })));
        });

        // Unsubscribe from the query when the component unmounts
        return () => unsubscribe();
    }, []);

    // Load more messages when the user scrolls up
    const loadMoreMessages = async () => {
        if (loading) return;

        setLoading(true);
        const collectionRef = collection(db, 'chatrooms', room, 'messages');
        const q = query(collectionRef, orderBy('createdAt', 'desc'), startAfter(lastVisible), limit(50));
        const snapshot = await getDocs(q);
        const moreMessagesExist = snapshot.docs.length > 0;

        // If more messages exist, update the lastVisible state and add the messages to the messages state
        if (moreMessagesExist) {
            setMessages(prevMessages => [
                ...prevMessages,
                ...snapshot.docs.map(doc => ({
                    _id: doc.id,
                    text: doc.data().text,
                    createdAt: doc.data().createdAt.toDate(),
                    user: doc.data().user,
                    image: doc.data().image,
                })),
            ]);

            // Update the lastVisible state to the last message in the snapshot
            const lastVisible = snapshot.docs[snapshot.docs.length - 1];
            setLastVisible(lastVisible);
        }
        // If no more messages exist, set loadEarlier to false
        setLoadEarlier(moreMessagesExist);
        setLoading(false);
    };

    // Create a function to pick an image from the device
    // and upload it to Firebase Storage
    const _pickImage = async () => {
        const { status } = await ImagePicker.requestMediaLibraryPermissionsAsync();
        if (status !== "granted") {
            alert("...");
            return;
        }
        let result = await ImagePicker.launchImageLibraryAsync({
            mediaTypes: ImagePicker.MediaTypeOptions.Images,
            allowsEditing: true,
            aspect: [4, 3],
            quality: 1,
        });

        if (!result.cancelled) {
            let uri = result.assets[0].uri;
            let storageRef = ref(storage, 'images/' + uri.split('/').pop());

            // Convert the URI to a Blob
            const response = await fetch(uri);
            const blob = await response.blob();
            let uploadTask = uploadBytesResumable(storageRef, blob);

            // Get the download URL
            uploadTask.then(() => {
                getDownloadURL(storageRef).then((downloadURL) => {
                    if (downloadURL) {
                        let newMessage = {
                            _id: Math.random().toString(36).substring(7),
                            createdAt: new Date(),
                            text: '',
                            user: {
                                _id: auth?.currentUser?.email,
                            },
                            image: downloadURL,
                        };
                        onSend([newMessage]);
                    } else {
                        console.log('No image URL available');
                    }
                }).catch(error => {
                    console.error("Error getting download URL: ", error);
                });
            }).catch(error => {
                console.error("Error uploading image: ", error);
            });
        }
    };

    // Create a function to render the message image
    const renderMessageImage = (props) => {
        return (
            <Image
                style={{ width: 150, height: 100 }}
                source={{ uri: props.currentMessage.image }}
            />
        );
    };

    // Create a function to send a message
    const onSend = useCallback((messages = []) => {
        setMessages(previousMessages => GiftedChat.append(previousMessages, messages));
        const { _id, createdAt, text, user, image } = messages[0];
        addDoc(collection(db, 'chatrooms', room, 'messages'), {
            _id,
            createdAt,
            text,
            user,
            image: image ? image : null,
        })
            .catch((err) => console.log(err));
    }, []);

    return (
        <GiftedChat
            messages={messages}
            onSend={messages => onSend(messages)}
            user={{
                _id: auth?.currentUser?.email,
            }}
            alwaysShowSend
            renderSend={props => <RenderSend {...props} onPickImage={_pickImage} />}
            renderMessage={RenderBubble}
            renderMessageImage={renderMessageImage}
            loadEarlier={loadEarlier}
            onLoadEarlier={loadMoreMessages}
            infiniteScroll
        />
    )
};
