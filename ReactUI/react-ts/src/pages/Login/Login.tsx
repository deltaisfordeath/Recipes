import React, {useEffect, useState} from "react";
import './Login.css'
import {useNavigate, useLocation, Link} from "react-router-dom";

export default function Login({setAuthToken}: { setAuthToken: React.Dispatch<React.SetStateAction<string>> }) {
    const [email, setEmail] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const [emailError, setEmailError] = useState<string>("");
    const [passwordError, setPasswordError] = useState<string>("");
    const [loginError, setLoginError] = useState<string>("");

    const navigate = useNavigate();
    const location = useLocation();

    async function submitLogin() {
        if (!email || !password) {
            if (!email) setEmailError("Please enter your email.");
            if (!password) setPasswordError("Please enter your password");
            return;
        }

        const auth = await fetch('login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                email: email,
                password: password
            })
        })

        if (auth.status === 200) {
            const data = await auth.json();
            setAuthToken(data.accessToken);
            navigate(location.state?.destination ?? '/recipes');
        } else {
            setLoginError("Invalid email or password");
        }
    }

    useEffect(() => {
        async function submitOnEnter(e: KeyboardEvent) {
            if (e.key === 'Enter') await submitLogin();
        }

        document.addEventListener('keydown', submitOnEnter);

        return () => document.removeEventListener('keydown', submitOnEnter);

    }, [submitLogin])

    return (<div className="login-container">
        Log in!
        {loginError && <div className="error-message">{loginError}</div>}
        <div className="login-input-group">
            <label htmlFor="email">Email</label>
            <input name="email" type="email" onChange={(e) => setEmail(e.target.value)} value={email}/>
            {emailError && <div className="error-message">{emailError}</div>}
        </div>
        <div className="login-input-group">
            <label htmlFor="password">Password</label>
            <input name="password-input" type="password" onChange={(e) => setPassword(e.target.value)}
                   value={password}/>
            {passwordError && <div className="error-message">{passwordError}</div>}
        </div>
        <button className="login-button" onClick={submitLogin}>Login</button>
        <div className="login-input-group">
            <Link to={"/signup"}>Register a New Account</Link>
        </div>
    </div>)
}