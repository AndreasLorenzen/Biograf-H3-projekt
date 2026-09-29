import '../App.css'
import { useState } from 'react';
import Popup from '../Components/popup';
import Navbar from '../Components/Navbar';
import { useNavigate } from 'react-router-dom';


export default function Homepage() {
    const [kontaktState, setKontaktState] = useState(false);
    const navigation = useNavigate()
  return (
    <>
    <Navbar/>

    <button onClick={() => setKontaktState(!kontaktState)}>Kontakt</button>
    {kontaktState === true && <Popup/>}
    <button onClick={() => navigation('/admin')}>Admin page</button>

    </>
  )
}

