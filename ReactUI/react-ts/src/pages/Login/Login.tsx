import React, {useEffect, useState} from "react";
import './Login.css'

export default function Login({setAuthToken}: {setAuthToken: React.Dispatch<React.SetStateAction<string>>}) {
    const [username, setUsername] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const [usernameError, setUsernameError] = useState<string>("");
    const [passwordError, setPasswordError] = useState<string>("");
    
    async function submitLogin() {
        if (!username || !password) {
            if (!username) setUsernameError("Please enter your username.");
            if (!password) setPasswordError("Please enter your password");
            return;
        }
        
        const auth = await fetch('login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                email: username, 
                password: password
            })
        })
        
        if (auth.status === 200) {
            const data = await auth.json();
            setAuthToken(data.accessToken);
        }
    }
    
    useEffect(() => {
        async function submitOnEnter(e: KeyboardEvent) {
            if (e.key === 'Enter') await submitLogin();
        }
        
        document.addEventListener('keydown', submitOnEnter);
        
        return () => document.removeEventListener('keydown', submitOnEnter);
        
    }, [submitLogin])
    
    return(<div className="login-container">
        Log in!
        <div className="login-input-group">
            <label htmlFor="username">Username</label>
            <input name="username-input" type="text" onChange={(e) => setUsername(e.target.value)} value={username}/>
            {usernameError && usernameError}
        </div>
        <div className="login-input-group">
            <label htmlFor="password">Password</label>
            <input name="password-input" type="password" onChange={(e) => setPassword(e.target.value)} value={password} />
            {passwordError && passwordError}
        </div>
        <button className="login-button" onClick={submitLogin}>Login</button>
    </div>)
}