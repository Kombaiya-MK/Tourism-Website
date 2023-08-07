import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import Footer from "./Layouts/Footer";
import Navbar from "./Layouts/Navbar";
import Packages from "./Components/Packages";
import Home from "./Components/Home";
import Profile from "./Components/Profile";
import CartPage from "./Components/CartPage";
import InvoicePage from "./Components/InvoicePage";
import AgencyApproval from "./Components/AgencyApproval";

const App = () => (
  <div className="container">
    <Navbar/>
    {/* <div>
      <Home />
    </div> */}
    {/* <Profile
      firstName="John"
      lastName="Doe"
      email="john@example.com"
      gender="male"
      age={30}
      walletAmount={500}
    />
    <Footer /> */}
  </div>
);

ReactDOM.render(<App />, document.getElementById("app"));
