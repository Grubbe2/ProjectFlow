// Import react modules
import React from 'react';
import {View, TouchableOpacity, StyleSheet, Text} from 'react-native';
import { GiftedChat, Send } from 'react-native-gifted-chat';
import {Icon} from 'react-native-elements';

// Export a function that renders the send button
export default function renderSend(props) {
    return (
        <View style={{flexDirection: 'row'}}>
            <TouchableOpacity onPress={props.onPickImage}>
                <Icon
                    type="font-awesome"
                    name="paperclip"
                    style={{
                    marginTop: 10,
                    marginRight: 15,
                    transform: [{rotateY: '180deg'}],
                    }}
                    size={20}
                    color='blue'
                    tvParallaxProperties={undefined}
                />
            </TouchableOpacity>
            <Send {...props}>
                <View>
                    <Text style={{
                        marginBottom: 10,
                        marginRight: 15,
                        color: 'blue',
                        fontWeight: 'bold',
                        fontSize: 18,
                    }}>Send</Text>
                </View>
            </Send>
        </View>
    );
  };

  