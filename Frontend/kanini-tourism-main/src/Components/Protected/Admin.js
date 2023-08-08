import { Navigate, useNavigate } from "react-router-dom";

function Admin({role, children})
{
    if(sessionStorage.getItem("role")!= null && sessionStorage.getItem("role") === "admin")
    {
        return children;
    }
    return <Navigate to="/"/>
}

export default Admin;
