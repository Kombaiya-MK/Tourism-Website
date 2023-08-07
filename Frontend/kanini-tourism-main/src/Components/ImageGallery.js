import React, { useState, useEffect } from 'react';
import {
  Typography,
  Paper,
  TextField,
  Button,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  ImageList,
  ImageListItem,
  ImageListItemBar,
} from '@mui/material';
import axios from 'axios';
import * as yup from 'yup';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const validationSchema = yup.object().shape({
  location: yup.string().required('Location is required'),
  images: yup.array().min(1, 'At least one image is required'),
});

const ImageGallery = () => {
  const [selectedLocation, setSelectedLocation] = useState('');
  const [imageFiles, setImageFiles] = useState([]);
  const [uploadedImages, setUploadedImages] = useState([]);
  const [locations, setLocations] = useState([]);
  const [locationName, setLocationName] = useState('');

  useEffect(() => {
    // Fetch locations data
    axios.get('https://localhost:7153/api/Location/GetAllLocations')
      .then(response => {
        setLocations(response.data);
      })
      .catch(error => {
        console.error('Error fetching locations:', error);
      });
  }, []);

  const handleLocationChange = (event) => {
    setSelectedLocation(event.target.value);
    const selectedLocationName = locations.find(location => location.locationId === event.target.value)?.name;
    setLocationName(selectedLocationName || '');
  };

  const handleImageChange = (event) => {
    const files = Array.from(event.target.files);
    setImageFiles(files);
  };

  const handleUpload = () => {
    // Validate the form before uploading
    validationSchema.validate({ location: selectedLocation, images: imageFiles })
      .then(() => {
        const uploaded = imageFiles.map((file) => ({
          location: locationName,
          url: URL.createObjectURL(file),
        }));
        setUploadedImages((prevImages) => [...prevImages, ...uploaded]);
        setImageFiles([]);

        // Show success toast
        toast.success('Images uploaded successfully');
      })
      .catch(error => {
        // Show error toast for each validation error
        error.inner.forEach(err => {
          toast.error(err.message);
        });

        // Show error toast for missing images
        if (imageFiles.length === 0) {
          toast.error('Please select at least one image');
        }
      });
  };

  return (
    <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', margin: '40px auto', maxWidth: 800 }}>
      <Paper elevation={3} style={{ padding: 20, width: '100%' }}>
        <Typography variant="h4" gutterBottom>
          Image Upload
        </Typography>
        <FormControl style={{ minWidth: 200, marginBottom: 20 }}>
          <InputLabel id="location-label">Location</InputLabel>
          <Select
            labelId="location-label"
            value={selectedLocation}
            onChange={handleLocationChange}
            label="Location"
          >
            {locations.map(location => (
              <MenuItem key={location.locationId} value={location.locationId}>
                {location.name}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
        <TextField
          type="file"
          multiple
          accept="image/*"
          onChange={handleImageChange}
        />
        <Button variant="contained" color="primary" onClick={handleUpload} style={{ marginTop: 10 }}>
          Upload
        </Button>
      </Paper>

      <div style={{ marginTop: 20, width: '100%' }}>
        <ImageList cols={4} gap={8}>
          {uploadedImages.map((image, index) => (
            <ImageListItem key={index}>
              <img src={image.url} alt={`Location: ${image.location}`} />
              <ImageListItemBar title={`Location: ${image.location}`} />
            </ImageListItem>
          ))}
        </ImageList>
      </div>

      <ToastContainer position="bottom-right" autoClose={3000} />
    </div>
  );
};

export default ImageGallery;
