import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Container, Typography, Card, CardContent, CardMedia, Grid, Button, Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions } from '@mui/material';
import { CarouselProvider, Slider, Slide, Dot } from 'pure-react-carousel';
import 'pure-react-carousel/dist/react-carousel.es.css';
import '../Assets/Styles/Home.css';

function DestinationCard({ destination, onClick }) {
  return (
    <Grid item xs={12} md={4}>
      <Card className="hover-card" onClick={() => onClick(destination)}>
        <CardMedia component="img" height={200} image={destination.image} alt={destination.name} />
        <CardContent>
          <Typography variant="h5">{destination.name}</Typography>
          <Typography variant="body2">{destination.description}</Typography>
          <Button variant="contained" color="primary">Explore</Button>
        </CardContent>
      </Card>
    </Grid>
  );
}

function Home() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedCard, setSelectedCard] = useState(null);
  const [destinations, setDestinations] = useState([]);
  const [experiences, setExperiences] = useState([]);

  useEffect(() => {
    // Fetch destinations data
    axios.get('https://localhost:7153/api/Location/GetAllLocations')
      .then(response => {
        const destinationData = response.data.map(destination => ({
          id: destination.id,
          name: destination.name,
          image: destination.images && destination.images[0], // Use the first image from the array
          description: destination.description
        }));
        setDestinations(destinationData);
      })
      .catch(error => {
        console.error('Error fetching destinations:', error);
      });

    axios.get('https://localhost:7153/api/Location/GetSpecialities?location=LOC001')
      .then(response => {
        const experienceData = response.data.map(experience => ({
          id: experience.id,
          name: experience.special,
          //image: experience.location.images && experience.location.images[0], // Use the first image from the location's images
          description: experience.description
        }));
        setExperiences(experienceData);
      })
      .catch(error => {
        console.error('Error fetching experiences:', error);
      });
  }, []);

  const handleCardClick = (card) => {
    setSelectedCard(card);
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setSelectedCard(null);
    setIsModalOpen(false);
  };

  const carouselSlides = [
    { id: 1, image: 'https://theplanetd.com/images/Cities-in-Japan.jpg', alt: 'Slide 1' },
    { id: 2, image: 'https://wallpapercave.com/wp/wp4118244.jpg', alt: 'Slide 2' },
  ];

  const knowledgeCards = [
    { id: 1, title: 'Japanese Culture', content: 'Learn about traditional tea ceremonies and kimono fashion.' },
    { id: 2, title: 'Culinary Delights', content: 'Indulge in sushi, ramen, and other mouthwatering dishes.' },
    { id: 3, title: 'Cherry Blossoms', content: 'Experience the beauty of cherry blossoms in spring.' },
    { id: 4, title: 'Samurai Heritage', content: 'Discover the history of samurai warriors and their influence.' },
    { id: 5, title: 'Shinto Shrines', content: 'Visit ancient Shinto shrines and experience spiritual rituals.' }
  ];

  const knowledgeCardColors = ['#f8a5c2', '#fad390', '#6a89cc', '#82ccdd', '#b8e994'];

  return (
    <div className="home-container">
      <section className="hero">
        <CarouselProvider
          naturalSlideWidth={100}
          naturalSlideHeight={60} 
          totalSlides={carouselSlides.length}
          isPlaying={true} 
        >
          <Slider>
            {carouselSlides.map(slide => (
              <Slide key={slide.id} index={slide.id - 1}>
                <img src={slide.image} alt={slide.alt} />
              </Slide>
            ))}
          </Slider>
          {carouselSlides.map(slide => (
            <Dot key={slide.id} slide={slide.id - 1}></Dot>
          ))}
        </CarouselProvider>
        <div className="hero-content">
          <h1>Welcome to Japan</h1>
          <p>Experience the Land of the Rising Sun</p>
        </div>
      </section>

      <section className="destinations">
        <Container>
          <Typography variant="h2" align="center" gutterBottom>
            Popular Destinations
          </Typography>
          <Grid container spacing={4}>
            {destinations.map((destination) => (
              <DestinationCard key={destination.id} destination={destination} onClick={handleCardClick} />
            ))}
          </Grid>
        </Container>
      </section>

      <section className="knowledge">
        <Container>
          <Typography variant="h2" align="center" gutterBottom>
            Knowledge
          </Typography>
          <Grid container spacing={4}>
            {knowledgeCards.map((card, index) => (
              <Grid item xs={12} md={4} key={card.id}>
                <Card className="hover-card" style={{ backgroundColor: knowledgeCardColors[index % knowledgeCardColors.length] }} onClick={() => handleCardClick(card)}>
                  <CardContent>
                    <Typography variant="h5">{card.title}</Typography>
                    <Typography variant="body2">{card.content}</Typography>
                  </CardContent>
                </Card>
              </Grid>
            ))}
          </Grid>
        </Container>
      </section>

      <section className="experiences">
        <Container>
          <Typography variant="h2" align="center" gutterBottom>
            Experiences
          </Typography>
          <Grid container spacing={4}>
            {experiences.map((experience) => (
              <Grid item xs={12} md={4} key={experience.id}>
                <Card className="hover-card" onClick={() => handleCardClick(experience)}>
                  <CardMedia component="img" height={200} image={experience.image} alt={experience.name} />
                  <CardContent>
                    <Typography variant="h5">{experience.name}</Typography>
                    <Typography variant="body2">{experience.description}</Typography>
                    <Button variant="contained" color="primary">Book Now</Button>
                  </CardContent>
                </Card>
              </Grid>
            ))}
          </Grid>
        </Container>
      </section>

      <Dialog open={isModalOpen} onClose={handleCloseModal} fullWidth maxWidth="sm">
        {selectedCard && (
          <>
            <DialogTitle>{selectedCard.name}</DialogTitle>
            <DialogContent>
              <DialogContentText>{selectedCard.description}</DialogContentText>
            </DialogContent>
            <DialogActions>
              <Button onClick={handleCloseModal} color="primary">
                Close
              </Button>
            </DialogActions>
          </>
        )}
      </Dialog>
    </div>
  );
}

export default Home;
