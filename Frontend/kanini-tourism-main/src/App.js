import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Navbar from './Layouts/Navbar'; // Update the path if needed
import Home from './Components/Home'; // Update the path if needed
import Packages from './Components/Packages'; // Update the path if needed
import Booking from './Components/Booking'; // Update the path if needed


function App() {

  return (
    <Navbar />
  );
}

export default App;