import React from "react";
import ReactDOM from "react-dom";

import "./index.css";
import Counter from "./Counter";

const App = () => (
  <div className="container">
    <div>Name: remote</div>
    <div>Framework: react</div>
    <div>Language: JavaScript</div>
    <div>CSS: Empty CSS</div>
    <Counter/>
  </div>
);
ReactDOM.render(<App />, document.getElementById("app"));
