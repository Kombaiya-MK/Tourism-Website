import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { CircularProgress, Typography } from '@mui/material';

function Logout() {
  const navigate = useNavigate();

  useEffect(() => {
    setTimeout(() => {
      navigate('/');
    }, 1500); 
  }, [navigate]);

  return (
    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
      <div>
        <CircularProgress size={40} style={{ marginBottom: '1rem' }} />
        <Typography variant="h6">Logging out...</Typography>
      </div>
    </div>
  );
}

export default Logout;
