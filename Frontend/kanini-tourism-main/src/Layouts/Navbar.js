import React, { useState } from 'react';
import {
  AppBar,
  Toolbar,
  IconButton,
  Drawer,
  List,
  ListItem,
  ListItemText,
  ListItemIcon,
  Divider,
  Typography,
  Tooltip,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  Button,
} from '@mui/material';
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
import PhotoLibraryIcon from '@mui/icons-material/PhotoLibrary';
import CloudUploadIcon from '@mui/icons-material/CloudUpload';
import AddCircleIcon from '@mui/icons-material/AddCircle';
import EventSeatIcon from '@mui/icons-material/EventSeat';
import LocalOfferIcon from '@mui/icons-material/LocalOffer';
import DescriptionIcon from '@mui/icons-material/Description';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import BookIcon from '@mui/icons-material/BookOnline'
import { Login, ShoppingCart } from '@mui/icons-material';
import { styled } from '@mui/system';
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
import LocationForm from '../Components/LocationForm';
import SpecialityInput from '../Components/SpecialityInput';
import ImageUpload from '../Components/ImageUpload';
import AdminPanel from '../Components/AdminPanel';
import Logout from '../Components/Logout';
import { Routes, Route, Link , useNavigate} from 'react-router-dom';
import ProfileCard from '../Components/Profile';
import AddTourPackageForm from '../Components/AddTourPackageForm'
import LoginForm from '../Components/LoginForm';
import AgencyForm from '../Components/AgencyForm'
import BookPackageForm from '../Components/BookPackageForm'
import ForgotPassword from '../Components/ForgotPassword';
import Modal from '@mui/material/Modal';
import VerifyCodeAndResetPassword from '../Components/VerifyCodeandResetPassword';


const sampleSelectedPacks = [
  { id: 1, name: 'Tokyo Tour', price: 300 },
  { id: 2, name: 'Kyoto Experience', price: 250 },
  { id: 3, name: 'Osaka Adventure', price: 180 },
];

const sampleTotalAmount = sampleSelectedPacks.reduce((total, pack) => total + pack.price, 0);

const Header = styled(AppBar)({
  backgroundColor: 'rgba(0, 0, 0, 0.7)',
  padding: '15px 0',
  position: 'fixed',
  top: 0,
  left: 0,
  right: 0,
  zIndex: 1000,
  boxShadow: 'none',
});

const Container = styled(Toolbar)({
  display: 'flex',
  justifyContent: 'space-between',
  alignItems: 'center',
});

const Logo = styled('div')({
  '& img': {
    width: '100px',
    height: 'auto',
  },
});

const NavLinks = styled('div')({
  display: 'flex',
  gap: '20px',
  listStyle: 'none',
});

const NavLinkItem = styled(ListItem)({
  position: 'relative',
});

const NavLink = styled(Link)({
  color: '#333',
  fontFamily: 'Open Sans, sans-serif',
  textDecoration: 'none',
  transition: 'color 0.3s ease-in-out',

  '&:hover': {
    color: '#555',
  },
});

const Dropdown = styled('div')({
  display: 'none',
  position: 'absolute',
  top: '100%',
  left: 0,
  backgroundColor: 'white',
  boxShadow: '0 2px 6px rgba(0, 0, 0, 0.1)',
  width: '200px',
  zIndex: 1001,
});

const DropdownItem = styled(ListItem)({
  padding: '10px 15px',
  borderBottom: '1px solid #ddd',
});

function Navbar({ userRole }) {
  const [isDrawerOpen, setIsDrawerOpen] = useState(false);
  const [isLogoutModalOpen, setIsLogoutModalOpen] = useState(false);
  const [isLoginModalOpen, setIsLoginModalOpen] = useState(false);
  const [isLoggedIn, setIsLoggedIn] = useState(
    localStorage.getItem('UserEmail') && localStorage.getItem('token')
  );

  const navigate = useNavigate();
  const navbarHeight = 100; 
  const childComponentStyles = {
    marginTop: `-${navbarHeight}px`,
  };

  const toggleDrawer = (open) => () => {
    setIsDrawerOpen(open);
  };

  const toggleLogoutModal = () => {
    setIsLogoutModalOpen(!isLogoutModalOpen);
  };

  const handleCloseLogoutModal = () => {
    setIsLogoutModalOpen(false);
  };

  const handleLogin = () => {
    setIsLoginModalOpen(true);
  };

  const handleCloseLoginModal = () => { 
    setIsLoginModalOpen(false);
  };

  const handleLogout = () => {
    localStorage.removeItem('UserEmail');
    localStorage.removeItem('token');
    setIsLoggedIn(false);
    setIsLogoutModalOpen(false);
    navigate('/');
  };

  const guestDrawerItems = [
    { text: 'Home', icon: <HomeIcon />, link: '/' },
    { text: 'Destinations', icon: <LocationOnIcon />, link: '/destinations' },
    { text: 'Experiences', icon: <FlightIcon />, link: '/experiences' },
    { text: 'Plan Your Trip', icon: <ExploreIcon />, link: '/plan-your-trip' },
    { text: 'Login', icon: <Login />, link: '/login' },
  ];

  const userDrawerItems = [
    { text: 'Home', icon: <HomeIcon />, link: '/' },
    { text: 'Destinations', icon: <LocationOnIcon />, link: '/destinations' },
    { text: 'Experiences', icon: <FlightIcon />, link: '/experiences' },
    { text: 'Plan Your Trip', icon: <ExploreIcon />, link: '/plan-your-trip' },
    { text: 'Cart', icon: <ShoppingCart />, link: '/cart' },
    { text: 'Bookings', icon: <EventSeatIcon />, link: '/booking' },
    { text: 'Profile', icon: <AccountCircleIcon />, link: '/profile' },
    { text: 'Logout', icon: <LogoutIcon />, onClick: toggleLogoutModal },
  ];

  const adminDrawerItems = [
    { text: 'Home', icon: <HomeIcon />, link: '/' },
    { text: 'Admin Panel', icon: <BusinessIcon />, link: '/admin' },
    { text: 'Agency Approval', icon: <PersonIcon />, link: '/agency-approval' },
    { text: 'Image Gallery', icon: <PhotoLibraryIcon />, link: '/image-gallery' },
    { text: 'Logout', icon: <LogoutIcon />, onClick: toggleLogoutModal },
  ];

  const agentDrawerItems = [
    { text: 'Home', icon: <HomeIcon />, link: '/' },
    { text: 'Agent Dashboard', icon: <PersonIcon />, link: '/agent' },
    { text: 'Booking', icon: <EventSeatIcon />, link: '/booking' },
    { text: 'Packages', icon: <LocalOfferIcon />, link: '/packages' },
    { text: 'Logout', icon: <LogoutIcon />, onClick: toggleLogoutModal },
  ];

  userRole = 'admin'
  //const isAgentApproved = userRole === 'agent' && true; 
  const isAgentApproved = true

  
  let drawerItems = [];
  if (!isLoggedIn) {
    drawerItems = userRole === 'admin'
      ? adminDrawerItems
      : (userRole === 'agent' ? (isAgentApproved ? agentDrawerItems : [{ text: 'Waiting Page', icon: <AccessTimeIcon />, link: '/waiting-page' }]) : userDrawerItems);
  } else {
    drawerItems = guestDrawerItems;
  }
  return (
    <div>
      <Header>
        <Container>
          <Logo>
            <img src="./resources/images/logo.png" alt="Japan Tourism" />
          </Logo>
          <NavLinks>
            <IconButton edge="start" color="inherit" aria-label="menu" onClick={toggleDrawer(true)}>
              <MenuIcon style={{ color: 'white' }} />
            </IconButton>
          </NavLinks>
        </Container>
      </Header>
      <Drawer anchor="right" open={isDrawerOpen} onClose={toggleDrawer(false)}>
        <div className="drawer-content">
          <div className="drawer-header">
            <Typography variant="h6" color="primary">
              Explore Japan
            </Typography>
          </div>
          <Divider />
          <IconButton edge="end" color="inherit" aria-label="close" onClick={toggleDrawer(false)}>
            <CloseIcon />
          </IconButton>
          <List>
            {drawerItems.map((item) => (
              <ListItem key={item.text} button component={Link} to={item.link} onClick={toggleDrawer(false)}>
                <ListItemIcon>
                  <Tooltip title={item.text} placement="right">
                    {item.icon}
                  </Tooltip>
                </ListItemIcon>
                <ListItemText primary={item.text} />
              </ListItem>
            ))}
          </List>
        </div>
      </Drawer>
      <Dialog open={isLoginModalOpen} onClose={handleCloseLoginModal}>
        <DialogTitle>Login</DialogTitle>
        <DialogContent>
          <DialogContentText>
            {/* Add your login form here */}
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseLoginModal} color="primary">
            Cancel
          </Button>
          {/* Add login button */}
        </DialogActions>
      </Dialog>
      <Dialog open={isLogoutModalOpen} onClose={handleCloseLogoutModal}>
        <DialogTitle>Logout</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Are you sure you want to logout?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseLogoutModal} color="primary">
            Cancel
          </Button>
          <Button onClick={handleLogout} color="primary">
            Logout
          </Button>
        </DialogActions>
      </Dialog>

      <Modal open={isLoginModalOpen} onClose={() => setIsLoginModalOpen(false)}>
        <div style={{ width: 400, backgroundColor: 'white', padding: 20, position: 'absolute', top: '50%', left: '50%', transform: 'translate(-50%, -50%)' }}>
          <LoginForm />
        </div>
      </Modal>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/packages" element={<Packages />} />
        <Route path="/profile" element={<ProfileCard />} />
        <Route path="/plan-your-trip" element={<Packages />} />
        <Route path="/cart" element={<CartPage />} />
        <Route path="/invoice" element={<InvoicePage selectedPacks={sampleSelectedPacks} totalAmount={sampleTotalAmount} />} />
        <Route path="/agency-approval" element={<AgencyApproval />} />
        <Route path="/image-gallery" element={<ImageGallery />} />
        <Route path="/agent" element={<AgentDashboard />} />
        <Route path="/booking" element={<Booking isAgent={userRole === 'agent'} />} />
        <Route path="/waiting-page" element={<WaitingPage />} />
        <Route path="/success-page" element={<SuccessPage />} />
        <Route path="/location-form" element={<LocationForm />} />
        <Route path="/speciality-input" element={<SpecialityInput />} />
        <Route path="/image-upload" element={<ImageUpload />} />
        <Route path="/admin" element={<AdminPanel />} />
        <Route path="/logout" element={<Logout />} />
        <Route path="/login" element={<LoginForm />} />
      </Routes>
      <Footer />
      {/* <div><LoginForm/></div>
        <div><AddTourPackageForm/></div>
        <div><BookPackageForm/></div>
        <div><AgencyForm/></div>
        <div><ForgotPassword/></div> */}
    </div>
  );
}

export default Navbar;
