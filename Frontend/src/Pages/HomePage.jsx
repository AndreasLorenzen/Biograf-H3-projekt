import '../App.css'
import { useState } from 'react';
import Popup from '../Components/popup';
import Navbar from '../Components/Navbar';


export default function Homepage() {
    const [kontaktState, setKontaktState] = useState(false);
  return (
    <>
    <Navbar/>

    <button onClick={() => setKontaktState(!kontaktState)}>Kontakt</button>
    {kontaktState === true && <Popup/>}

    </>
  )
}

