import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import '../App.css'
import Navbar from '../Components/Navbar'

// I den her fil gør vi brug af fetch til at hente data, og render det på siden

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

    <main className="PageShell">
    <section className="PageIntro">
        <h1>Hej</h1>
        <p>Her ser du oversigten over film der spilles.</p>
    </section>
    
    <div className='ProgramContent'>
        <div className="ProgramList">
            {movieData.map((movie) => (
                <article className="ProgramItem" key={movie.movieId}>
                    <h2>{movie.movieName}</h2>
                    <p>Varighed: {movie.movieDuration} minutter</p>
                    <button onClick={() => navigate('/payment', { state: { movie } })}>Book film</button>
                </article>
            ))}
        </div>
    </div>
    </main>

    </>
    )
}