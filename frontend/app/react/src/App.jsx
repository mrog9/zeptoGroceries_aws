import { useState } from 'react'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import LoginForm from './components/LoginForm'
import SignupForm from './components/SignupForm'
import { SearchProducts } from './pages/customer/SearchProducts'
import './App.css'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LoginForm />} />
        <Route path="/signup" element={<SignupForm />} />
        <Route path="/searchProducts" element={<SearchProducts />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
