import { useNavigate } from 'react-router-dom';
import '../App.css'
import Navbar from '../Components/Navbar';


export default function ErrorPage() {
    const navigate = useNavigate()
  return (
    <>
    <Navbar/>
    <main className="PageShell ErrorContent">
        <h1>Error 404</h1>
        <p>Side findes ikke</p>
        <button onClick={() => navigate('/')}>Tilbage til Home</button>
    </main>
    
    </>
  )
}

