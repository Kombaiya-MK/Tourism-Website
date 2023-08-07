import React, { useState } from 'react';
import { Container, Typography, Button, TextField } from '@mui/material';
import { styled } from '@mui/system';
import axios from 'axios';

const InputContainer = styled('div')({
  paddingTop: '2rem',
  paddingBottom: '2rem',
  textAlign: 'center',
});

function ImageUpload() {
  const [selectedFile, setSelectedFile] = useState(null);
  const [uploading, setUploading] = useState(false);
  const [uploadError, setUploadError] = useState(null);

  const handleFileChange = (event) => {
    setSelectedFile(event.target.files[0]);
  };

  const handleUpload = async () => {
    if (!selectedFile) {
      return;
    }

    try {
      setUploading(true);
      setUploadError(null);

      const blobServiceUrl = 'http://127.0.0.1:10000/devstoreaccount1';
      const containerName = 'images'; 
      const uploadUrl = `${blobServiceUrl}/${containerName}/${selectedFile.name}`;

      const response = await axios.put(uploadUrl, selectedFile, {
        headers: {
          'x-ms-blob-type': 'BlockBlob',
        },
      });

      if (response.status === 201) {
        const imageUrl = uploadUrl;
        const locationId = 'LOC001'; 

        const addImageUrl = 'https://localhost:7153/api/Location/AddImage';
        await axios.post(addImageUrl, { locationId, picture: imageUrl });

        setUploading(false);
        setSelectedFile(null);
      }
    } catch (error) {
      setUploadError('Failed to upload image. Please try again.');
      setUploading(false);
    }
  };

  return (
    <InputContainer>
      <Container>
        <Typography variant="h4" gutterBottom>
          Upload Image for Location
        </Typography>
        <input type="file" accept="image/*" onChange={handleFileChange} />
        <Button variant="contained" color="primary" onClick={handleUpload} disabled={uploading || !selectedFile}>
          Upload
        </Button>
        {uploading && <Typography variant="body2">Uploading...</Typography>}
        {uploadError && <Typography color="error">{uploadError}</Typography>}
      </Container>
    </InputContainer>
  );
}

export default ImageUpload;
