import React, { useState, useEffect } from 'react';
import { Container, Typography, List, ListItem, ListItemText, ListItemIcon, IconButton, TextField, Button, Input, Modal, Tooltip } from '@mui/material';
import { styled } from '@mui/system';
import EditIcon from '@mui/icons-material/Edit';
import AddCircleIcon from '@mui/icons-material/AddCircle';
import axios from 'axios';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const AdminContainer = styled('div')({
  paddingTop: '2rem',
  paddingBottom: '2rem',
  textAlign: 'center',
});

function AdminPanel() {
  const [locations, setLocations] = useState([]);
  const [selectedLocation, setSelectedLocation] = useState(null);
  const [specialityInput, setSpecialityInput] = useState('');
  const [specialityDescriptionInput, setSpecialityDescriptionInput] = useState('');
  const [locationNameInput, setLocationNameInput] = useState('');
  const [imageInput, setImageInput] = useState(null);
  const [submitting, setSubmitting] = useState(false);
  const [submitError, setSubmitError] = useState(null);
  const [editModalOpen, setEditModalOpen] = useState(false);
  const [addLocationModalOpen, setAddLocationModalOpen] = useState(false);

  useEffect(() => {
    fetchLocations();
  }, []);

  const fetchLocations = async () => {
    try {
      const response = await axios.get('https://localhost:7153/api/Location/GetAllLocations');
      if (response.status === 200) {
        setLocations(response.data);
      }
    } catch (error) {
      console.error(error);
    }
  };

  const handleAddSpeciality = async () => {
    if (!selectedLocation || !specialityInput || !specialityDescriptionInput) return;

    try {
      setSubmitting(true);
      setSubmitError(null);

      const newSpeciality = {
        locationId: selectedLocation.locationId,
        locationName: selectedLocation.name, // Add locationName
        speciality: specialityInput,
        description: specialityDescriptionInput,
      };

      const response = await axios.post('https://localhost:7153/api/Location/AddSpeciality', newSpeciality);

      if (response.status === 201) {
        setSelectedLocation({
          ...selectedLocation,
          specialities: [...(selectedLocation.specialities || []), specialityInput],
        });
        setSpecialityInput('');
        setSpecialityDescriptionInput('');
        setSubmitting(false);

        toast.success('Speciality added successfully!', {
          position: toast.POSITION.TOP_CENTER,
        });
      }
    } catch (error) {
      setSubmitting(false);
      setSubmitError(error.message);

      toast.error('Error adding speciality.', {
        position: toast.POSITION.TOP_CENTER,
      });
    }
  };

  const handleAddImage = async () => {
    if (!selectedLocation || !imageInput) return;

    try {
      setSubmitting(true);
      setSubmitError(null);

      const formData = new FormData();
      formData.append('locationId', selectedLocation.locationId);
      formData.append('picture', imageInput);

      const response = await axios.post('https://localhost:7153/api/Location/AddImage', formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      });

      if (response.status === 201) {
        setSelectedLocation({
          ...selectedLocation,
          images: [...(selectedLocation.images || []), response.data.picture],
        });
        setImageInput(null);
        setSubmitting(false);

        toast.success('Image added successfully!', {
          position: toast.POSITION.TOP_CENTER,
        });
      }
    } catch (error) {
      setSubmitting(false);
      setSubmitError(error.message);

      toast.error('Error adding image.', {
        position: toast.POSITION.TOP_CENTER,
      });
    }
  };

  const handleAddLocation = async () => {
    if (!locationNameInput) return;

    try {
      setSubmitting(true);
      setSubmitError(null);

      const newLocation = {
        locationName: locationNameInput,
      };

      const response = await axios.post('https://localhost:7153/api/Location/AddLocation', newLocation);

      if (response.status === 201) {
        fetchLocations();
        setLocationNameInput('');
        setAddLocationModalOpen(false);

        toast.success('Location added successfully!', {
          position: toast.POSITION.TOP_CENTER,
        });
      }
    } catch (error) {
      setSubmitting(false);
      setSubmitError(error.message);

      toast.error('Error adding location.', {
        position: toast.POSITION.TOP_CENTER,
      });
    }
  };

  const handleEditLocation = () => {
    // Implement your edit location logic here
  };

  return (
    <AdminContainer>
      <Container>
        <Typography variant="h4" gutterBottom>
          Admin Panel
        </Typography>
        <List>
          {locations.map((location) => (
            <ListItem key={location.id}>
              <ListItemText
                primary={location.name}
                secondary={location.description}
              />
              <ListItemIcon>
                <IconButton onClick={() => setSelectedLocation(location)}>
                  <EditIcon />
                </IconButton>
              </ListItemIcon>
            </ListItem>
          ))}
        </List>
        <div>
          <Button variant="contained" color="primary" onClick={() => setAddLocationModalOpen(true)}>
            Add New Location
          </Button>
        </div>
        {selectedLocation && (
          <div>
            <Typography variant="h6">Selected Location: {selectedLocation.name}</Typography>
            <div>
              <Typography variant="subtitle1">Specialities:</Typography>
              <ul>
                {selectedLocation.specialities && selectedLocation.specialities.map((speciality, index) => (
                  <li key={index}>{speciality}</li>
                ))}
              </ul>
              <TextField
                label="Add Speciality"
                fullWidth
                margin="normal"
                value={specialityInput}
                onChange={(e) => setSpecialityInput(e.target.value)}
              />
              <TextField
                label="Speciality Description"
                fullWidth
                margin="normal"
                value={specialityDescriptionInput}
                onChange={(e) => setSpecialityDescriptionInput(e.target.value)}
              />
              <Tooltip title="Add Speciality">
                <Button variant="contained" color="primary" onClick={handleAddSpeciality} disabled={submitting}>
                  Add Speciality
                </Button>
              </Tooltip>
            </div>
            <div>
              <Typography variant="subtitle1">Images:</Typography>
              <ul>
                {selectedLocation.images && selectedLocation.images.map((image, index) => (
                  <li key={index}>{image}</li>
                ))}
              </ul>
              <Input
                type="file"
                accept="image/*"
                onChange={(e) => setImageInput(e.target.files[0])}
              />
              <Tooltip title="Add Image">
                <Button variant="contained" color="primary" onClick={handleAddImage} disabled={submitting}>
                  Add Image
                </Button>
              </Tooltip>
            </div>
            <div>
              {/* Edit Location Modal */}
              <Modal open={editModalOpen} onClose={() => setEditModalOpen(false)}>
                <div>
                  {/* Implement your edit location form */}
                  {/* You can use Material-UI components and form logic here */}
                  <Button variant="contained" color="primary" onClick={handleEditLocation}>
                    Save Changes
                  </Button>
                </div>
              </Modal>
            </div>
            {submitError && <Typography color="error">{submitError}</Typography>}
          </div>
        )}
        {/* Add Location Modal */}
        <Modal open={addLocationModalOpen} onClose={() => setAddLocationModalOpen(false)}>
          <div>
            <Typography variant="h6">Add New Location</Typography>
            <TextField
              label="Location Name"
              fullWidth
              margin="normal"
              value={locationNameInput}
              onChange={(e) => setLocationNameInput(e.target.value)}
            />
            <Button variant="contained" color="primary" onClick={handleAddLocation} disabled={submitting}>
              Add Location
            </Button>
          </div>
        </Modal>
      </Container>
      <ToastContainer position={toast.POSITION.TOP_CENTER} />
    </AdminContainer>
  );
}

export default AdminPanel;
