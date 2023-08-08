import { Navigate, useNavigate } from "react-router-dom";

function Agent({role, children})
{
    if(sessionStorage.getItem("role")!= null && sessionStorage.getItem("role") === "agent")
    {
        return children;
    }
    return <Navigate to="/"/>
}

export default Agent;
