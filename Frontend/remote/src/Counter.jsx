import { useState } from "react";

function Counter(){
    var [count , setCount] = useState(1)
    return(
        <div>
            <button onClick={() => {
                setCount(count+1)
            }}>click</button>
            <div><em><h1>{count}</h1></em></div>
        </div>
    )
}

export default Counter;