import React, { useState } from 'react';
import {
  Container,
  Typography,
  Grid,
  Button,
  Card,
  CardContent,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
} from '@mui/material';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import EditIcon from '@mui/icons-material/Edit';
import { styled } from '@mui/material/styles';
import '../Assets/Styles/AgentDashboard.css';

const StyledAddIcon = styled(AddCircleOutlineIcon)(({ theme }) => ({
  color: theme.palette.success.main,
}));

const StyledEditIcon = styled(EditIcon)(({ theme }) => ({
  color: theme.palette.primary.main,
}));

var itinerariesData = [
  {
    packageId: 1,
    day: 1,
    time: '10:00 AM',
    location: 'City Center',
    description: 'Explore the city',
  },
  {
    packageId: 1,
    day: 2,
    time: '1:00 PM',
    location: 'Museum',
    description: 'Visit the local museum',
  },
  {
    packageId: 2,
    day: 1,
    time: '11:00 AM',
    location: 'Beach',
    description: 'Relax at the beach',
  },
  // Add more itinerary data
];

function AgentDashboard() {
  const [packages, setPackages] = useState([
    {
      id: 1,
      name: 'Tokyo Adventure',
      description: 'Explore the vibrant city of Tokyo',
    },
    {
      id: 2,
      name: 'Beach Getaway',
      description: 'Relax on the beautiful sandy beaches',
    },
    {
      id: 3,
      name: 'Historical Europe',
      description: 'Experience the rich history of European cities',
    },
    // Add more package data
  ]);

  const [openPackageDialog, setOpenPackageDialog] = useState(false);
  const [selectedPackage, setSelectedPackage] = useState(null);
  const [packageName, setPackageName] = useState('');
  const [packageDescription, setPackageDescription] = useState('');
  const [itineraryTime, setItineraryTime] = useState('');
  const [itineraryLocation, setItineraryLocation] = useState('');
  const [itineraryDescription, setItineraryDescription] = useState('');
  const [itineraryDay, setItineraryDay] = useState('');

  const handlePackageOpen = (packageId) => {
    setSelectedPackage(packageId);
    setOpenPackageDialog(true);
  };

  const handlePackageClose = () => {
    setOpenPackageDialog(false);
    setSelectedPackage(null);
    setPackageName('');
    setPackageDescription('');
    setItineraryTime('');
    setItineraryLocation('');
    setItineraryDescription('');
    setItineraryDay('');
  };

  const handleEditPackage = () => {
    // TODO: Implement edit package functionality
    // Update the package data
    const updatedPackages = packages.map((pkg) =>
      pkg.id === selectedPackage ? { ...pkg, name: packageName, description: packageDescription } : pkg
    );
    setPackages(updatedPackages);

    handlePackageClose();
  };

  const handleAddPackage = () => {
    const newPackage = {
      id: packages.length + 1,
      name: packageName,
      description: packageDescription,
    };

    setPackages([...packages, newPackage]);
    handlePackageClose();
  };

  const handleAddItinerary = () => {
    // TODO: Implement add itinerary functionality
    // Update the itinerariesData with the new itinerary
    const newItinerary = {
      packageId: selectedPackage,
      day: parseInt(itineraryDay),
      time: itineraryTime,
      location: itineraryLocation,
      description: itineraryDescription,
    };
    const updatedItineraries = [...itinerariesData, newItinerary];
    itinerariesData = updatedItineraries;

    setItineraryTime('');
    setItineraryLocation('');
    setItineraryDescription('');
    setItineraryDay('');
  };

  return (
    <div className="agent-dashboard">
      <Container>
        <Typography variant="h2" align="center" gutterBottom>
          Agent Dashboard
        </Typography>
        <div className="filter-section">
          <Grid container spacing={2}>
            <Grid item xs={12} md={4}>
              <TextField fullWidth variant="outlined" label="Search Packages" />
            </Grid>
            {/* Add more filter options here */}
          </Grid>
        </div>
        <div className="package-list">
          <Grid container spacing={4}>
            {packages.map((pkg) => (
              <Grid item xs={12} md={4} key={pkg.id}>
                <Card>
                  <CardContent>
                    <Typography variant="h5">{pkg.name}</Typography>
                    <Typography variant="body2">{pkg.description}</Typography>
                    <div className="action-buttons">
                      <Button onClick={() => handlePackageOpen(pkg.id)}>
                        <StyledEditIcon />
                      </Button>
                    </div>
                  </CardContent>
                </Card>
              </Grid>
            ))}
            <Grid item xs={12} md={4}>
              <Card>
                <CardContent>
                  <Button onClick={handleAddPackage} fullWidth>
                    <StyledAddIcon />
                    Add Package
                  </Button>
                </CardContent>
              </Card>
            </Grid>
          </Grid>
        </div>
      </Container>
      {/* Package Details Dialog */}
      <Dialog open={openPackageDialog} onClose={handlePackageClose} maxWidth="sm" fullWidth>
        <DialogTitle>Package Details</DialogTitle>
        <DialogContent>
          {selectedPackage !== null && (
            <div>
              <TextField
                fullWidth
                variant="outlined"
                label="Package Name"
                value={packageName}
                onChange={(e) => setPackageName(e.target.value)}
              />
              <TextField
                fullWidth
                variant="outlined"
                label="Package Description"
                value={packageDescription}
                onChange={(e) => setPackageDescription(e.target.value)}
                multiline
                rows={4}
              />
              {/* Add more fields for editing package details */}
              <div>
                <Typography variant="h6">Itineraries</Typography>
                {itinerariesData
                  .filter((it) => it.packageId === selectedPackage)
                  .map((it) => (
                    <div key={`${it.day}-${it.time}`}>
                      <Typography variant="body2">{`Day ${it.day}: ${it.time} - ${it.location} - ${it.description}`}</Typography>
                    </div>
                  ))}
                <TextField
                  fullWidth
                  variant="outlined"
                  label="Day"
                  value={itineraryDay}
                  onChange={(e) => setItineraryDay(e.target.value)}
                />
                <TextField
                  fullWidth
                  variant="outlined"
                  label="Time"
                  value={itineraryTime}
                  onChange={(e) => setItineraryTime(e.target.value)}
                />
                <TextField
                  fullWidth
                  variant="outlined"
                  label="Location"
                  value={itineraryLocation}
                  onChange={(e) => setItineraryLocation(e.target.value)}
                />
                <TextField
                  fullWidth
                  variant="outlined"
                  label="Description"
                  value={itineraryDescription}
                  onChange={(e) => setItineraryDescription(e.target.value)}
                  multiline
                  rows={4}
                />
                <Button onClick={handleAddItinerary} color="primary">
                  Add Itinerary
                </Button>
              </div>
            </div>
          )}
        </DialogContent>
        <DialogActions>
          <Button onClick={handlePackageClose} color="primary">
            Cancel
          </Button>
          <Button onClick={handleEditPackage} color="primary">
            Save Changes
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}

export default AgentDashboard;
