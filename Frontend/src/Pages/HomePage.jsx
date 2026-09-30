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

    <main className="PageShell HomeContent">
      <section className="PageIntro">
        <h1>Velkommen til Andreas Bio</h1>
        <p>Find den næste filmoplevelse i biografens program.</p>
      </section>
      <div className="HomeActions">
        <button onClick={() => setKontaktState(!kontaktState)} aria-expanded={kontaktState}>Kontakt</button>
        <button onClick={() => navigation('/admin')}>Adminside</button>
      </div>
      {kontaktState && <div className="ContactDetails"><Popup/></div>}
    </main>

    </>
  )
}

