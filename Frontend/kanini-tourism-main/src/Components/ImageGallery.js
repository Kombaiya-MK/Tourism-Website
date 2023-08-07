import React, { useState } from 'react';
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

const ImageGallery = () => {
  const [selectedLocation, setSelectedLocation] = useState('');
  const [imageFiles, setImageFiles] = useState([]);
  const [uploadedImages, setUploadedImages] = useState([]);

  const handleLocationChange = (event) => {
    setSelectedLocation(event.target.value);
  };

  const handleImageChange = (event) => {
    const files = Array.from(event.target.files);
    setImageFiles(files);
  };

  const handleUpload = () => {
    // Logic to upload images to the selected location
    // Here, we are just simulating the upload process
    const uploaded = imageFiles.map((file) => ({
      location: selectedLocation,
      url: URL.createObjectURL(file),
    }));
    setUploadedImages((prevImages) => [...prevImages, ...uploaded]);
    setImageFiles([]);
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
            <MenuItem value="Tokyo">Tokyo</MenuItem>
            <MenuItem value="Kyoto">Kyoto</MenuItem>
            <MenuItem value="Osaka">Osaka</MenuItem>
            {/* Add more locations */}
          </Select>
        </FormControl>
        <input type="file" multiple accept="image/*" onChange={handleImageChange} />
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
    </div>
  );
};

export default ImageGallery;
