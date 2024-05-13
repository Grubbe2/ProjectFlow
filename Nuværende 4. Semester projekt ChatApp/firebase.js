// Purpose: Firebase configuration and initialization
import { initializeApp } from 'firebase/app';
import { getFirestore } from 'firebase/firestore';
import { getAuth } from 'firebase/auth';

//Firebase configuration
const firebaseConfig = {
  apiKey: "AIzaSyCY98Bmyiwy8muqVUqK9SCW2IfeGIYvPAI",
  authDomain: "pentia-chatapp-28cff.firebaseapp.com",
  projectId: "pentia-chatapp-28cff",
  storageBucket: "pentia-chatapp-28cff.appspot.com",
  messagingSenderId: "139477706395",
  appId: "1:139477706395:web:cd41c955fd5fabeb4ac83e"
};
  

// Initialize Firebase
initializeApp(firebaseConfig);
export const db = getFirestore();
export const auth = getAuth();

//iOS = 139477706395-5fdlo5u54pfa6o35lb1c8iqlmnu5fna6.apps.googleusercontent.com
//Android = 139477706395-nk4rsssf25ii6gas2ajsvls6i0uaf64b.apps.googleusercontent.com