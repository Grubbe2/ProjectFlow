// Import react modules
import React, { useState, createContext, useContext, useEffect } from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
import { StyleSheet, Text, View, ActivityIndicator } from 'react-native';

// Import firebase modules
import { onAuthStateChanged } from 'firebase/auth';
import { auth } from './firebase';

// Import screens
import Chat from './screens/Chat';
import Login from './screens/Login';
import Signup from './screens/Signup';
import Home from './screens/Home';


// Create a stack navigator and a context
const Stack = createStackNavigator();
const AuthenticatedUserContext = createContext({});

// Create a provider for the authenticated user context
const AuthenticatedUserProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  return (
    <AuthenticatedUserContext.Provider value={{ user, setUser }}>
      {children}
    </AuthenticatedUserContext.Provider>
  );
};

// Create a stack navigator for the chat screens
function ChatStack() {
  return (
      <Stack.Navigator defaultScreenOptions={Home}>
        <Stack.Screen name="Home" component={Home} />
        <Stack.Screen name="Chat" component={Chat} />
      </Stack.Navigator>
  );
};

// Create a stack navigator for the authentication screens
function AuthStack() {
  return (
    <Stack.Navigator defaultScreenOptions={Login} screenOptions={{
      headerShown: false
    }}>
      <Stack.Screen name="Login" component={Login} />
      <Stack.Screen name="Signup" component={Signup} />
    </Stack.Navigator>
  );
};

// Create a root navigator that displays the chat stack if the user is authenticated, 
// and the auth stack if the user is not authenticated
function RootNavigator() {
  const { user, setUser } = useContext(AuthenticatedUserContext);
  const [isLoading, setIsLoading] = useState(true);
  
  // Check if the user is authenticated
  useEffect(() => {
    const unsubscribe = onAuthStateChanged(
      auth, 
      async authenticatedUser => {
        authenticatedUser ? setUser(authenticatedUser) : setUser(null);
        setIsLoading(false);
      }
    );
    return unsubscribe;
  }, [user]);
  
  // Display a loading indicator while checking the user's authentication status
  if (isLoading) {
    return (
      <View style={styles.container}>
        <ActivityIndicator size='large' />
      </View>
    );
  }
  return (
    <NavigationContainer>
      {user ? <ChatStack /> : <AuthStack />}
    </NavigationContainer>
  );
};

// Export the App component
export default function App() {
  return (
    <AuthenticatedUserProvider>
      <RootNavigator />
    </AuthenticatedUserProvider>
  );
  
};

// Create the styles
const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
  },
});
