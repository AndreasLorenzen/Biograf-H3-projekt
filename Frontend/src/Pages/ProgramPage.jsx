import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import '../App.css'
import Navbar from '../Components/Navbar'


export default function Program() {
    const navigate = useNavigate()
    const [movieData, setMovieData] = useState([])
    useEffect(() => {
        const getData = async () => {
            const response = await fetch(`/api/Movie/getMovie`)
            const data = await response.json()
            console.log(data)
            setMovieData(Array.isArray(data) ? data : [])
        }
        getData()
    },[])
    return (
    <>
    <Navbar/>

    <div>
        <h1>Hej</h1>
        <p>Her ser du oversigten over film der spilles</p>
    </div>
    
    
    <div className='ProgramContent'>
        {movieData.map((movie) => (
            <div key={movie.movieId}>
                <h1>{movie.movieName}</h1>
                <p>{movie.movieDuration}</p>
                <button onClick={() => navigate('/payment', { state: { movie } })}>Book film</button>
            </div>
        ))}
    </div>
    

    </>
    )
}