import React from 'react';
import { Container, Typography, Button } from '@mui/material';
import { Link } from 'react-router-dom';
import { styled } from '@mui/system';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';

const SuccessContainer = styled('div')({
  paddingTop: '2rem',
  paddingBottom: '2rem',
  textAlign: 'center',
});

function SuccessPage() {
  return (
    <SuccessContainer>
      <Container>
        <CheckCircleIcon style={{ fontSize: '5rem', color: '#4caf50', marginBottom: '1rem' }} />
        <Typography variant="h2" gutterBottom>
          Request Approved
        </Typography>
        <Typography variant="body1" paragraph>
          Congratulations! Your request has been successfully approved by the agent.
        </Typography>
        <Button  variant="contained" color="primary">
        {/* component={Link} to="/agent-dashboard" */}
          Go to Dashboard
        </Button>
      </Container>
    </SuccessContainer>
  );
}

export default SuccessPage;
