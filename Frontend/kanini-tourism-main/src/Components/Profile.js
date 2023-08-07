import React, { useState, useEffect } from 'react';
import {
  Card,
  CardContent,
  CardActions,
  IconButton,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Button,
  Typography,
} from '@mui/material';
import {
  Edit as EditIcon,
  AccountBalanceWallet as WalletIcon,
  Person as PersonIcon,
  Email as EmailIcon,
  LocationOn as LocationIcon,
  AddCircleOutline as AddCircleOutlineIcon,
} from '@mui/icons-material';
import AgencyForm from './AgencyForm';

function ProfileCard() {
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [isApplyForAgencyModalOpen, setIsApplyForAgencyModalOpen] = useState(false);
  const [editedData, setEditedData] = useState({});
  const [profileData, setProfileData] = useState({});
  const [email, setEmail] = useState('');

  useEffect(() => {
    // Fetch user profile data based on the email stored in local storage
    const storedEmail = localStorage.getItem('userEmail');
    if (storedEmail) {
      setEmail(storedEmail);
      fetchProfileData(storedEmail);
    }
  }, []);

  const fetchProfileData = async (email) => {
    try {
      const response = await fetch(`https://localhost:7289/api/User/GetAllUserDetails?email=${email}`);
      const data = await response.json();
      setProfileData(data);
    } catch (error) {
      console.error('Error fetching profile data:', error);
    }
  };

  const cardStyle = {
    width: '300px',
    margin: '0 auto',
    marginTop: '2rem',
    boxShadow: '0 4px 8px rgba(0, 0, 0, 0.1)',
    borderRadius: '8px',
    textAlign: 'center',
  };

  const cardContentStyle = {
    padding: '1rem',
  };

  const iconStyle = {
    fontSize: '2rem',
    marginBottom: '0.5rem',
  };

  const buttonStyle = {
    marginTop: '1rem',
  };

  const handleEdit = () => {
    setIsEditModalOpen(true);
  };

  const handleSave = () => {
    // Implement your save logic here
    setIsEditModalOpen(false);
    // Update the user profile data in the parent component or make an API call
    console.log('Edited Data:', editedData);
  };

  const handleCancel = () => {
    setIsEditModalOpen(false);
  };

  const handleApplyForAgency = () => {
    setIsApplyForAgencyModalOpen(true);
  };

  const handleCancelApplyForAgency = () => {
    setIsApplyForAgencyModalOpen(false);
  };

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setEditedData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  return (
    <Card style={cardStyle}>
      <CardContent style={cardContentStyle}>
        <PersonIcon style={iconStyle} />
        <Typography variant="h6">
          {profileData.userDetails?.[0]?.userName}
        </Typography>
        <EmailIcon style={iconStyle} />
        <Typography variant="subtitle1">{email}</Typography>
        <LocationIcon style={iconStyle} />
        <Typography variant="subtitle1">{profileData.userDetails?.[0]?.address}</Typography>
        <WalletIcon style={iconStyle} />
        <Typography variant="subtitle1">Wallet: ${profileData.walletAmount}</Typography>
      </CardContent>
      <CardActions>
        <IconButton onClick={handleEdit}>
          <EditIcon />
        </IconButton>
        <IconButton onClick={handleApplyForAgency}>
          <AddCircleOutlineIcon />
        </IconButton>
      </CardActions>

      {/* Edit Profile Modal */}
      <Dialog open={isEditModalOpen} onClose={handleCancel} fullWidth maxWidth="sm">
        <DialogTitle>Edit Profile</DialogTitle>
        <DialogContent>
          <TextField
            label="First Name"
            fullWidth
            name="firstName"
            value={profileData.userDetails?.[0]?.firstName || ''}
            onChange={handleInputChange}
            margin="normal"
          />
          <TextField
            label="Last Name"
            fullWidth
            name="lastName"
            value={profileData.userDetails?.[0]?.lastName || ''}
            onChange={handleInputChange}
            margin="normal"
          />
          {/* Additional fields such as email, gender, age, etc. */}
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCancel} color="primary">
            Cancel
          </Button>
          <Button onClick={handleSave} color="primary">
            Save
          </Button>
        </DialogActions>
      </Dialog>

      {/* Apply For Agency Modal */}
      <Dialog open={isApplyForAgencyModalOpen} onClose={handleCancelApplyForAgency} fullWidth maxWidth="sm">
        <DialogTitle>Apply For Agency</DialogTitle>
        <DialogContent>
          {<AgencyForm/>}
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCancelApplyForAgency} color="primary">
            Cancel
          </Button>
        </DialogActions>
      </Dialog>
    </Card>
  );
}

export default ProfileCard;
