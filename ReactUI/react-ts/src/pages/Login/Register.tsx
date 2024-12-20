import {useEffect, useState} from "react";
import {useDebounce} from "../../hooks/useDebounce.tsx";
import {emailRegex} from "../../constants.js";

export default function Register() {
    const [userName, setUserName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    
    const debouncedEmail = useDebounce(email, 300);

    useEffect( () => {
        async function checkEmail() {
            const stringEmail = debouncedEmail.toString();
            if (stringEmail && emailRegex.test(stringEmail)) {
                const emailResponse = await fetch(`/Api/CheckEmail?email=${encodeURIComponent(stringEmail)}`);
                const emailExists = await emailResponse.json();
                if (emailExists) {
                    setError(`Account with email '${stringEmail}' already exists.`);
                }
            } else {
                setError("");
            }
            
        }
        checkEmail();
        
    }, [debouncedEmail]);

    async function handleSubmit() {
        if (!email || !password || !userName) {
            setError('Please complete all fields.');
            return;
        }
        
        if (emailRegex.test(email)) {
            setError('Please enter a valid email');
            return;
        }

        try {
            const response = await fetch('/register', {
                method: 'POST',
                headers: {
                    "Content-Type": 'application/json',
                },
                body: JSON.stringify({
                    email,
                    userName,
                    password,
                })
            });
            if (!response.ok) {
                const json = await response.json();
                const errorMessages = Object.values(json.errors)
                    .map(error => (error as string[]).join('\n')).join("\n");
                setError(errorMessages);
            }
        } catch (error) {
            setError(error as unknown as string);
        }
    }
    
    return(<div>
        {error && <div className={'error-message'}>{error}</div>}
        <div className="login-input-group">
            <label htmlFor="userName">Username</label>
            <input name="userName" id="userName" type="text" placeholder="Username" onChange={(e) => setUserName(e.target.value)}/>
        </div>
        <div className="login-input-group">
            <label htmlFor="email">Email</label>
            <input name="email" id="email" type="email" placeholder="Email" onChange={(e) => setEmail(e.target.value)}/>
        </div>
        <div className="login-input-group">
            <label htmlFor="password">Password</label>
            <input name="password" id="password" type="password" placeholder="Password"
                   onChange={(e) => setPassword(e.target.value)}/>
        </div>
        <button type={'button'} onClick={handleSubmit}>Register</button>
    </div>)
}