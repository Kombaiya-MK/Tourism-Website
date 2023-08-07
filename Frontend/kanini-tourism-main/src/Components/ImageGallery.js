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
import { BlobServiceClient } from '@azure/storage-blob';

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
    const AZURITE_BLOB_SERVICE_URL = 'http://localhost:10000';
    const ACCOUNT_NAME = 'devstoreaccount1';
    const ACCOUNT_KEY = 'Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==';

    const blobServiceClient = new BlobServiceClient(
      "http://127.0.0.1:10000/devstoreaccount1/locations?sv=2018-03-28&st=2023-08-07T21%3A18%3A28Z&se=2023-08-08T21%3A18%3A28Z&sr=c&sp=racwdl&sig=PPX0G2c4rQ4a1QoJec12sKbMD5FOCls92W03FxEkkSs%3D",
      "?sv=2018-03-28&st=2023-08-07T21%3A18%3A28Z&se=2023-08-08T21%3A18%3A28Z&sr=c&sp=racwdl&sig=PPX0G2c4rQ4a1QoJec12sKbMD5FOCls92W03FxEkkSs%3D"
    );

    const containerClient = blobServiceClient.getContainerClient('locations');
    for(let i = 0; i < imageFiles.length;i++)
    {
      const blobClient = containerClient.getBlobClient(imageFiles[i].name);
            const blockBlobClient = blobClient.getBlockBlobClient();
            const result = blockBlobClient.uploadBrowserData(imageFiles[i], {
                blockSize: 4 * 1024 * 1024,
                concurrency: 20,
                onProgress: ev => console.log(ev)
            });
    }


    validationSchema.validate({ location: selectedLocation, images: imageFiles })
      .then(() => {
        const formData = new FormData();
        formData.append('locationId', selectedLocation);
        imageFiles.forEach(file => {
          formData.append('picture', imageFiles.name);
        });

        axios.post('https://localhost:7153/api/Location/AddImage', formData, {
          headers: {
            'Content-Type': 'multipart/form-data',
          },
        })
          .then(response => {
            const uploaded = response.data.map(image => ({
              location: locationName,
              url: image,
            }));
            setUploadedImages((prevImages) => [...prevImages, ...uploaded]);
            setImageFiles([]);
            toast.success('Images uploaded successfully');
          })
          .catch(error => {
            console.error('Error uploading images:', error);
            toast.error('An error occurred while uploading images');
          });
      })
      .catch(error => {
        error.inner.forEach(err => {
          toast.error(err.message);
        });
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
