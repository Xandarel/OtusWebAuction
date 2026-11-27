import { Link, Route, Routes } from 'react-router'
import SingUp from './components/SignUp.tsx'
import HomePage from './components/HomePage.tsx'
import './App.css'

function App() {
    return (
        <>
            <header className="topbar">
                <Link to="/">Аукцион</Link>
                <nav>
                    <Link to="/login">Вход</Link>
                    <Link to="/signup">Регистрация</Link>
                </nav>
            </header>
            <main>
                <Routes>
                    <Route path="/" element={<HomePage />} />
                    <Route path="/signup" element={<SingUp />} />
                    <Route path="/login" element={<p>Страница входа</p>} />
                </Routes>
            </main>
        </>
    )
}
export default App