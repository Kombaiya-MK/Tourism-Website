import { Navigate, useNavigate } from "react-router-dom";

function User({role, children})
{
    if(sessionStorage.getItem("role")!= null && sessionStorage.getItem("role") === "user")
    {
        return children;
    }
    return <Navigate to="/"/>
}

export default User;
