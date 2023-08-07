import React from "react";
import ReactDOM from "react-dom";

import "./index.css";
import Login from "./Components/Login";
import AgencyForm from "./Components/AgencyForm";
import AddTourPackageForm from "./Components/AddTourPackageForm";
import BookPackageForm from "./Components/BookPackageForm";


const App = () => (
  <div className="container">
    <Login/>
    <AgencyForm/>
    <AddTourPackageForm/>
    <BookPackageForm/>
  </div>
);
ReactDOM.render(<App />, document.getElementById("app"));
