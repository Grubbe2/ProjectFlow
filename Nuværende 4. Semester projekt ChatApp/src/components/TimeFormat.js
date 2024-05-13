// Import react modules
import React from 'react';

// Import moment module
import moment from 'moment';

// Create a function that formats the time
const timeFormat = (date) => {
    const messageDate = moment(date);
    const today = moment();
    let formattedDate;

    // Check if the message was sent today
    if (messageDate.isSame(today, 'day')) {
        // If the message was sent today, format the date as 'HH:mm'
        formattedDate = messageDate.format('HH:mm');
    } else {
        // If the message was sent earlier than today, format the date as 'Mon DD, YYYY'
        formattedDate = messageDate.format('ll');
    }
    return formattedDate;
};

export { timeFormat };