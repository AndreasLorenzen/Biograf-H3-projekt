import '../App.css'
import { useNavigate } from 'react-router-dom';

export default function Navbar(){
        const navigate = useNavigate()

    return (
     <>
        <div className='commonheader'>
            <div className='commonheaderleft'>
            <h1>Andreas BIO</h1>
            </div>
            <div className='commonheaderright'>
            <button id='buttonstyle' onClick={() => navigate('/login')}>Login</button>
            <button id='buttonstryle' >Program</button>
            </div>
        </div>
     </>   
    )
}