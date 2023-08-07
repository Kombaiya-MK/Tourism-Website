import React, { useState } from 'react';
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
import { Edit as EditIcon, AccountBalanceWallet as WalletIcon, Person as PersonIcon, Email as EmailIcon, LocationOn as LocationIcon } from '@mui/icons-material';

function ProfileCard({ firstName, lastName, email, gender, age, walletAmount }) {
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [editedData, setEditedData] = useState({
    firstName: firstName,
    lastName: lastName,
    email: email,
    gender: gender,
    age: age,
    walletAmount: walletAmount,
  });

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
          {firstName} {lastName}
        </Typography>
        <EmailIcon style={iconStyle} />
        <Typography variant="subtitle1">{email}</Typography>
        <LocationIcon style={iconStyle} />
        <Typography variant="subtitle1">City, Country</Typography>
        <WalletIcon style={iconStyle} />
        <Typography variant="subtitle1">Wallet: ${walletAmount}</Typography>
      </CardContent>
      <CardActions>
        <IconButton onClick={handleEdit}>
          <EditIcon />
        </IconButton>
      </CardActions>

      <Dialog open={isEditModalOpen} onClose={handleCancel} fullWidth maxWidth="sm">
        <DialogTitle>Edit Profile</DialogTitle>
        <DialogContent>
          <TextField
            label="First Name"
            fullWidth
            name="firstName"
            value={editedData.firstName}
            onChange={handleInputChange}
            margin="normal"
          />
          <TextField
            label="Last Name"
            fullWidth
            name="lastName"
            value={editedData.lastName}
            onChange={handleInputChange}
            margin="normal"
          />
          <TextField
            label="Email"
            fullWidth
            name="email"
            value={editedData.email}
            onChange={handleInputChange}
            margin="normal"
          />
          {/* Additional fields such as gender, age, etc. */}
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
    </Card>
  );
}

export default ProfileCard;
