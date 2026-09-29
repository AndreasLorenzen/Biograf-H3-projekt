import '../App.css'
import { Link, useLocation } from 'react-router-dom'
import { useEffect, useState } from 'react'
import Navbar from '../Components/Navbar'
import { useAuth } from '../Components/Authcontext.jsx'


export default function Payment() {
  const { state } = useLocation()
  const { bruger } = useAuth()
  const movie = state?.movie
  const [customer, setCustomer] = useState({ name: '', email: '' })
  const [paymentComplete, setPaymentComplete] = useState(false)

  useEffect(() => {
    setCustomer({
      name: bruger?.username ?? '',
      email: bruger?.email ?? '',
    })
  }, [bruger])

  function handlePayment(event) {
    event.preventDefault()
    setPaymentComplete(true)
  }

  return (
    <>
      <Navbar />
      <main className="PaymentContent">
        {movie ? (
          <>
            <section className="PaymentMovie">
              <h1>{movie.movieName}</h1>
              <p>Varighed: {movie.movieDuration} minutter</p>
            </section>

            <form className="PaymentForm" onSubmit={handlePayment}>
              <h2>Kontaktoplysninger</h2>
              <label className="PaymentField">
                Navn
                <input
                  autoComplete="name"
                  required
                  value={customer.name}
                  onChange={(event) => setCustomer({ ...customer, name: event.target.value })}
                />
              </label>
              <label className="PaymentField">
                Email
                <input
                  autoComplete="email"
                  required
                  type="email"
                  value={customer.email}
                  onChange={(event) => setCustomer({ ...customer, email: event.target.value })}
                />
              </label>

              <h2>Fiktiv betaling</h2>
              <label className="PaymentField">
                Kortnummer
                <input autoComplete="cc-number" inputMode="numeric" maxLength={19} placeholder="1234 5678 9012 3456" required />
              </label>
              <div className="PaymentCardGrid">
                <label className="PaymentField">
                  Udløbsdato
                  <input autoComplete="cc-exp" type="month" required />
                </label>
                <label className="PaymentField">
                  CVC
                  <input autoComplete="cc-csc" inputMode="numeric" maxLength={4} placeholder="123" required />
                </label>
              </div>
              <button className="PaymentSubmit" type="submit">Betal (demo)</button>
              {paymentComplete && <p role="status">Demobetalingen er gennemført for {movie.movieName}.</p>}
            </form>
          </>
        ) : (
          <>
            <h1>Vælg en film</h1>
            <Link to="/program">Gå til programmet</Link>
          </>
        )}
      </main>
    </>
  )
}