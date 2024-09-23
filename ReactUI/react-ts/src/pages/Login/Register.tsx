import {useState} from "react";

export default function Register() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    async function handleSubmit() {
        if (!email || !password) {
            setError('Please complete all fields.');
            return
        }

        try {
            const response = await fetch('/register', {
                method: 'POST',
                headers: {
                    "Content-Type": 'application/json',
                },
                body: JSON.stringify({
                    email,
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
            <label htmlFor="email">Email</label>
            <input name="email" id="email" type="email" placeholder="Email" onChange={(e) => setEmail(e.target.value)}/>
        </div>
        <div className="login-input-group">
            <label htmlFor="password">Password</label>
            <input name="password" id="password" type="password" placeholder="Password" onChange={(e) => setPassword(e.target.value)}/>
        </div>
            <button type={'button'} onClick={handleSubmit}>Register</button>
    </div>)
}