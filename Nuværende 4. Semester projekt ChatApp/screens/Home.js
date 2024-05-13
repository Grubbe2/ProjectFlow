// Import the necessary modules
import React, { useEffect, useState, useCallback } from "react";
import { View, TouchableOpacity, Text, StyleSheet, RefreshControl, ScrollView } from "react-native";
import { useNavigation } from "@react-navigation/native";
import { Icon } from 'react-native-elements';
import { signOut } from 'firebase/auth';
import { auth, db } from '../firebase';
import { collection, orderBy, query, limit, getDocs } from 'firebase/firestore';
import { timeFormat }  from "../src/components/TimeFormat";

// Create the Home component
export default function Home() {
    const [chatRooms, setChatRooms] = useState([]);
    const [refreshing, setRefreshing] = useState(false);
    const navigation = useNavigation();

    // Create a function to handle the sign out
    const onSignOut = () => {
        signOut(auth)
        .catch(error => console.log(error));
    };

    // Create a function to fetch the chat rooms and their last message data
    const fetchChatRooms = async () => {
        const collectionRef = collection(db, 'chatrooms');
        const snapshot = await getDocs(collectionRef);
        const chatRoomsData = await Promise.all(snapshot.docs.map(async doc => {
            const messagesRef = collection(db, 'chatrooms', doc.id, 'messages');
            const lastMessageQuery = query(messagesRef, orderBy('createdAt', 'desc'), limit(1));
            const lastMessageSnapshot = await getDocs(lastMessageQuery);
            const lastMessage = lastMessageSnapshot.docs[0]?.data();
            return { id: doc.id, lastMessage, ...doc.data() }
        }));
        chatRoomsData.sort((a, b) => b.lastMessage.createdAt - a.lastMessage.createdAt);
        setChatRooms(chatRoomsData);
    };

    // Create a function to refresh the chat rooms
    const onRefresh = useCallback(() => {
        setRefreshing(true);
        fetchChatRooms();
        setRefreshing(false);
    }, []);

    // Fetch the chat rooms when the component mounts
    useEffect(() => {
        fetchChatRooms();
    }, []);

    // Set the header right button to a logout button
    useEffect(() => {
        navigation.setOptions({
            headerRight: () => (
                <TouchableOpacity onPress={onSignOut}>
                    <Text style={styles.logoutButton} >Logout</Text>
                </TouchableOpacity>
            ),
        });
    }, [navigation]);

    // Return a list of the chat rooms
    return (
        <ScrollView
            contentContainerStyle={{ flex: 1 }}
            refreshControl={
                <RefreshControl
                    refreshing={refreshing}
                    onRefresh={onRefresh}
                />
            }
        >
            <View style={styles.container}>
                {chatRooms.map((room) => (
                    <TouchableOpacity
                        key={room.id}
                        onPress={() => {
                            navigation.navigate("Chat", {
                                roomId: room.id,
                            });
                        }}
                        style={styles.chatButton}
                    >
                        <View style={styles.chatButtonFull}>
                            <View style={styles.chatButtonContent}>
                                <Text style={styles.chatButtonText}>{room.id}</Text>
                                <Text style={styles.chatButtonMessage}>
                                    {room.lastMessage?.text.length > 15
                                        ? `${room.lastMessage.text.substring(0, 15)}...`
                                        : room.lastMessage?.text}</Text>
                            </View>
                            <Text style={styles.chatButtonTimestamp}>
                                {room.lastMessage ? timeFormat(room.lastMessage.createdAt.toDate()) : ''}
                            </Text>
                            <Icon name='chevron-right' type='font-awesome' color='black' />
                        </View>
                    </TouchableOpacity>
                ))}
            </View>
        </ScrollView>
    );
};

// Create the styles
const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'flex-start',
        alignItems: 'flex-start',
        backgroundColor: "#fff",
    },
    chatButtonFull: {
        flex: 1,
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'space-between',
        paddingHorizontal: 10,
    },
    chatButton: {
        width: '95%',
        height: 60,
        borderRadius: 25,
        backgroundColor: '#7018ee',
        marginTop: 20,
        marginLeft: 10,
        marginBottom: 20,
    },
    chatButtonContent: {
        flex: 1,
        flexDirection: 'column',
        alignItems: 'left',
        justifyContent: 'space-between',
        paddingHorizontal: 10,
        marginLeft: 10,
    },
    chatButtonText: {
        fontSize: 20,
        color: 'black',
    },
    chatButtonMessage: {
        fontSize: 16,
        color: 'lightblue',
    },
    chatButtonTimestamp: {
        fontSize: 14,
        color: 'lightblue',
        marginBottom: 20,
        marginRight: 10,

    },
    logoutButton: {
        marginRight: 10,
        fontSize: 20,
    },

});