// NavBar.js
import React, { useState } from 'react';
import '../Assets/Styles/NavBar.css';

const Navbar = () => {
  const [showMenu, setShowMenu] = useState(false);

  const toggleMenu = () => {
    setShowMenu(!showMenu);
  };

  return (
    <nav className="navbar">
      <div className="navbar-container">
        <div className="logo">Make Your Trip</div>
        <div className={`menu-icon ${showMenu ? 'open' : ''}`} onClick={toggleMenu}>
          <div className="bar"></div>
          <div className="bar"></div>
          <div className="bar"></div>
        </div>
        <div className={`nav-links ${showMenu ? 'active' : ''}`}>
          <div className="nav-item" onClick={toggleMenu}>
            Home
          </div>
          <div className="nav-item" onClick={toggleMenu}>
            About
          </div>
          <div className="nav-item dropdown" onMouseEnter={toggleMenu} onMouseLeave={toggleMenu}>
            <span className="dropdown-trigger">Destinations</span>
            <div className={`dropdown-content ${showMenu ? 'active' : ''}`}>
              <div className="dropdown-item" onClick={toggleMenu}>
                Tokyo
              </div>
              <div className="dropdown-item" onClick={toggleMenu}>
                Kyoto
              </div>
            </div>
          </div>
          <div className="nav-item" onClick={toggleMenu}>
            Activities
          </div>
          <div className="nav-item" onClick={toggleMenu}>
            Contact
          </div>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
