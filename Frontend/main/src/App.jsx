import React from "react";
import ReactDOM from "react-dom";

import "./index.css";
import Navbar from "./Layouts/Navbar";

const App = () => (
  <div className="container">
    <Navbar/>
  </div>
);
ReactDOM.render(<App />, document.getElementById("app"));
