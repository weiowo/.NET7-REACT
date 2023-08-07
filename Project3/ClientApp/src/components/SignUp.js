import { useEffect, useState } from "react"

function SignUp() {

    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const [role, setRole] = useState("")
    const [confirmPassword, setConfirmPassword] = useState("");


    useEffect(() => {
        fetch('api/Auth/SignUp', {
            method: "POST",
            body: JSON.stringify({
                userName: "Test",
                password: "123",
                confirmPassword: "123",
                role: "hey",
                isActive:1

            }),
        }).then((res) => res.json())
        .then((data) => console.log(data))
    },[])


    return (
        <div>
            <span> name:
                <input value={userName} onChange={(e) => setUserName(e.target.value)} />
            </span>
            <span> role:
                <input value={role} onChange={(e) => setRole(e.target.value)} />
            </span>
            <span> password:
                <input value={password} onChange={(e) => setPassword(e.target.value)} />
            </span>
            <span> confirmPassword:
                <input value={confirmPassword} onChange={(e) => setConfirmPassword(e.target.value)} />
            </span>
        </div>
        )
}

export default SignUp