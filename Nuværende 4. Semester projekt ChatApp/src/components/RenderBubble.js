// Import react modules
import React from 'react';
import { Text, View } from 'react-native';
import { Bubble } from 'react-native-gifted-chat';

// Export a function that renders the chat bubble
export default function renderBubble(props) {
  return (
    <Bubble
      {...props}
      wrapperStyle={{
        right: {
          backgroundColor: '#7018ee',
          margin: 5,
        },
        left: {
          backgroundColor: '#E5E5EA',
          margin: 5,
        },
      }}
      textStyle={{
        right: {
          color: 'white',
        },
        left: {
          color: '#000000',
        },
      }}
      // Render the user id
      renderCustomView={() => <Text
        style={{
          color: 'black',
          fontSize: 12,
          textAlign: 'right',
          marginRight: 10,
          marginBottom: 5,
          marginTop: 5,
          marginLeft: 10,
        }}
      >
        {props.currentMessage.user._id}
      </Text>}
    />
  );
};