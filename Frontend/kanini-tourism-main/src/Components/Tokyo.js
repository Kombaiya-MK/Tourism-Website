import React from 'react';
import { Container, Typography, Card, CardContent, CardMedia, Button } from '@mui/material';
import { Link } from 'react-router-dom';
import '../Assets/Styles/DestinationPage.css';

function Tokyo() {
  return (
    <div className="destination-page">
      <section className="hero">
        <img src="https://placeimg.com/1200/400/arch" alt="Tokyo" />
        <div className="hero-content">
          <h1>Tokyo</h1>
          <p>Explore the Vibrant City</p>
        </div>
      </section>

      <section className="details">
        <Container>
          <Typography variant="h2" align="center" gutterBottom>
            Tokyo - Explore the Vibrant City
          </Typography>
          <Typography variant="body1" gutterBottom>
            Tokyo, the capital of Japan, is a sprawling metropolis known for its futuristic technology, vibrant pop culture, and historical landmarks. With a unique blend of traditional temples and modern skyscrapers, Tokyo offers a diverse range of experiences for visitors.
          </Typography>
          <Typography variant="h4" gutterBottom>
            Specifications:
          </Typography>
          <ul>
            <li>Population: Approximately 13.5 million</li>
            <li>Language: Japanese</li>
            <li>Currency: Japanese Yen (JPY)</li>
          </ul>
          <Link to="/" className="back-link">
            <Button variant="outlined" color="primary">Back to Home</Button>
          </Link>
        </Container>
      </section>

      {/* ... (additional sections for the destination) ... */}
    </div>
  );
}

export default Tokyo;
