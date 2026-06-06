import { useState } from 'react'
import { useNavigate, Link } from 'react-router-dom'
import '../styles/Auth.css'

function LoginForm() {
  const [username, setUsername] = useState('')
  const [error, setError] = useState('')
  const navigate = useNavigate()

  const handleSubmit = (e) => {
    e.preventDefault()
    
    if (!username.trim()) {
      setError('Username is required')
      return
    }

    // Simulate login
    console.log('Logging in with username:', username)
    setError('')

    // Redirect to dashboard (or wherever after login)
    navigate('/searchProducts', {state: {currentUser: username}})
  }

  return (
    <div className="auth-container">
      <div className="auth-hero">
        <h1 className="auth-title">Welcome back to Zepto Groceries — Freshness at Zepto Speed</h1>
        <h4 className="auth-tagline">Freshness delivered faster — sign in to shop in a Zepto</h4>
      </div>
      <div className="auth-card">
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="username">Username</label>
            <input
              id="username"
              type="text"
              placeholder="Enter your username"
              value={username}
              onChange={(e) => {
                setUsername(e.target.value)
                setError('')
              }}
              className="form-input"
            />
          </div>
          
          {error && <div className="error-message">{error}</div>}
          
          <button type="submit" className="btn btn-primary">
            Login
          </button>
        </form>
        
        <p className="auth-link">
          Don't have an account? <Link to="/signup">Create one</Link>
        </p>
      </div>
    </div>
  )
}

export default LoginForm
