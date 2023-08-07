import React, { useState } from 'react';
import { AppBar, Toolbar, IconButton, Drawer, List, ListItem, ListItemText, ListItemIcon, Divider, Typography } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import CloseIcon from '@mui/icons-material/Close';
import HomeIcon from '@mui/icons-material/Home';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import FlightIcon from '@mui/icons-material/Flight';
import ExploreIcon from '@mui/icons-material/Explore';
import InfoIcon from '@mui/icons-material/Info';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import LogoutIcon from '@mui/icons-material/Logout';
import PersonIcon from '@mui/icons-material/Person';
import BusinessIcon from '@mui/icons-material/Business';
import PeopleIcon from '@mui/icons-material/People';
import '../Assets/Styles/Navbar.css';
import Home from '../Components/Home';
import Footer from './Footer';
import Packages from '../Components/Packages';
import CartPage from '../Components/CartPage';
import InvoicePage from '../Components/InvoicePage';
import AgencyApproval from '../Components/AgencyApproval';
import ImageGallery from '../Components/ImageGallery';
import AgentDashboard from '../Components/AgentDashboard';
import Booking from '../Components/Booking';
import WaitingPage from '../Components/WaitingPage';
import SuccessPage from '../Components/SuccessPage';

const sampleSelectedPacks = [
  { id: 1, name: 'Tokyo Tour', price: 300 },
  { id: 2, name: 'Kyoto Experience', price: 250 },
  { id: 3, name: 'Osaka Adventure', price: 180 },
];

const sampleTotalAmount = sampleSelectedPacks.reduce((total, pack) => total + pack.price, 0);

function Navbar({ userRole }) {
  const [isDrawerOpen, setIsDrawerOpen] = useState(false);

  const toggleDrawer = (open) => () => {
    setIsDrawerOpen(open);
  };

  return (
    <div>
      <AppBar position="fixed" className="header" style={{ backgroundColor: 'rgba(0, 0, 0, 0.7)', boxShadow: 'none' }}>
        <Toolbar className="container">
          <div className="logo">
            <img src="./resources/images/logo.png" alt="Japan Tourism" />
          </div>
          <div className="nav-links">
            <IconButton edge="start" color="inherit" aria-label="menu" onClick={toggleDrawer(true)}>
              <MenuIcon style={{ color: 'white' }} />
            </IconButton>
          </div>
        </Toolbar>
        <Drawer anchor="right" open={isDrawerOpen} onClose={toggleDrawer(false)}>
          <div className="drawer-content">
            <div className="drawer-header">
              <Typography variant="h6" color="primary">Explore Japan</Typography>
            </div>
            <Divider />
            <IconButton edge="end" color="inherit" aria-label="close" onClick={toggleDrawer(false)}>
              <CloseIcon />
            </IconButton>
            <List>
              <ListItem button component="a" href="/" onClick={toggleDrawer(false)}>
                <ListItemIcon><HomeIcon /></ListItemIcon>
                <ListItemText primary="Home" />
              </ListItem>
              <ListItem button component="a" href="/destinations" onClick={toggleDrawer(false)}>
                <ListItemIcon><LocationOnIcon /></ListItemIcon>
                <ListItemText primary="Destinations" />
              </ListItem>
              <ListItem button component="a" href="/experiences" onClick={toggleDrawer(false)}>
                <ListItemIcon><FlightIcon /></ListItemIcon>
                <ListItemText primary="Experiences" />
              </ListItem>
              <ListItem button component="a" href="/plan-your-trip" onClick={toggleDrawer(false)}>
                <ListItemIcon><ExploreIcon /></ListItemIcon>
                <ListItemText primary="Plan Your Trip" />
              </ListItem>
              <ListItem button component="a" href="/about-japan" onClick={toggleDrawer(false)}>
                <ListItemIcon><InfoIcon /></ListItemIcon>
                <ListItemText primary="About Japan" />
              </ListItem>
              {userRole === 'user' && (
                <ListItem button component="a" href="/" onClick={toggleDrawer(false)}>
                  <ListItemIcon><AccountCircleIcon /></ListItemIcon>
                  <ListItemText primary="Profile" />
                </ListItem>
              )}
              {userRole === 'admin' && (
                <ListItem button component="a" href="/admin" onClick={toggleDrawer(false)}>
                  <ListItemIcon><BusinessIcon /></ListItemIcon>
                  <ListItemText primary="Admin" />
                </ListItem>
              )}
              {userRole === 'agent' && (
                <ListItem button component="a" href="/agent" onClick={toggleDrawer(false)}>
                  <ListItemIcon><PersonIcon /></ListItemIcon>
                  <ListItemText primary="Agent" />
                </ListItem>
              )}
              <ListItem button component="a" href="/" onClick={toggleDrawer(false)}>
                <ListItemIcon><LogoutIcon /></ListItemIcon>
                <ListItemText primary="Logout" />
              </ListItem>
            </List>
          </div>
        </Drawer>
      </AppBar>
      <div>
        <Home />
      </div>
      <div>
        <Packages />
      </div>
      <div>
        <CartPage />
      </div>
      <div>
        <InvoicePage selectedPacks={sampleSelectedPacks} totalAmount={sampleTotalAmount} />
      </div>
      <div>
        <AgencyApproval />
      </div>
      <div>
        <ImageGallery />
      </div>
      <div>
        <AgentDashboard />
      </div>
      <div><Booking isAgent={userRole === 'agent'} /></div>
      <div>
        <WaitingPage />
      </div>
      <div>
        <SuccessPage />
      </div>
      <div>
        <Footer />
      </div>
    </div>
  );
}

export default Navbar;
