import React, { useState } from 'react';
import { Container, Typography, Card, CardContent, CardMedia, Grid, Button, Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions } from '@mui/material';
import { CarouselProvider, Slider, Slide, Dot } from 'pure-react-carousel';
import 'pure-react-carousel/dist/react-carousel.es.css';
import '../Assets/Styles/Home.css';
import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

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

  const destinations = [
    { id: 1, name: 'Tokyo', image: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTxU-qxU4SctDt8E8VBc-kLVuwdbCyqfmifSg&usqp=CAU', description: 'Explore the vibrant city of Tokyo.' },
    { id: 2, name: 'Kyoto', image: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSVbsZMVz90LGfJoOVgxPd5yU2AuOn7f6AX_w&usqp=CAU', description: 'Discover the ancient charm of Kyoto.' },
    { id: 3, name: 'Osaka', image: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTJmxahzibgb89Dk5J9WFn6CONfg9g5q7RX-A&usqp=CAU', description: 'Experience modern culture in Osaka.' },
    { id: 4, name: 'Hiroshima', image: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQCxs9KgSKfBFngsftoXVRD_u-LAzmSSAef8A&usqp=CAU', description: 'Visit Hiroshima and its historical sites.' },
    { id: 5, name: 'Nara', image: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRe-Q8Yx1MRQ-4qZdJsypjQGjWsznUkqrWKIQ&usqp=CAU', description: 'Meet friendly deer in Nara.' },
    { id: 6, name: 'Okinawa', image: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTVDoDlxmTQq2qA8bnlK1cAsZl3CczNljxblQ&usqp=CAU', description: 'Relax on the beautiful beaches of Okinawa.' }
  ];

  const experiences = [
    { id: 1, name: 'Mount Fuji Hike', image: 'https://via.placeholder.com/300', description: 'Experience the breathtaking beauty of Mount Fuji.' },
    { id: 2, name: 'Traditional Tea Ceremony', image: 'https://via.placeholder.com/300', description: 'Participate in a serene traditional tea ceremony.' },
    { id: 3, name: 'Sumo Wrestling', image: 'https://via.placeholder.com/300', description: 'Witness the excitement of sumo wrestling matches.' },
    { id: 4, name: 'Ghibli Museum', image: 'https://via.placeholder.com/300', description: 'Immerse yourself in the world of Studio Ghibli.' },
    { id: 5, name: 'Sushi Cooking Class', image: 'https://via.placeholder.com/300', description: 'Learn to make delicious sushi in a traditional class.' },
    { id: 6, name: 'Zen Meditation Retreat', image: 'https://via.placeholder.com/300', description: 'Find inner peace through Zen meditation in a serene temple.' }
  ];

  const knowledgeCards = [
    { id: 1, title: 'Japanese Culture', content: 'Learn about traditional tea ceremonies and kimono fashion.' },
    { id: 2, title: 'Culinary Delights', content: 'Indulge in sushi, ramen, and other mouthwatering dishes.' },
    { id: 3, title: 'Cherry Blossoms', content: 'Experience the beauty of cherry blossoms in spring.' },
    { id: 4, title: 'Samurai Heritage', content: 'Discover the history of samurai warriors and their influence.' },
    { id: 5, title: 'Shinto Shrines', content: 'Visit ancient Shinto shrines and experience spiritual rituals.' }
  ];

  const knowledgeCardColors = ['#f8a5c2', '#fad390', '#6a89cc', '#82ccdd', '#b8e994'];

  const handleCardClick = (card) => {
    setSelectedCard(card);
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setSelectedCard(null);
    setIsModalOpen(false);
  };

  return (
    <div className="home-container">
      <section className="hero">
        <CarouselProvider
          naturalSlideWidth={100}
          naturalSlideHeight={60} // Increase image height
          totalSlides={3}
          isPlaying={true} // Auto-play carousel
        >
          <Slider>
            <Slide index={0}>
              <img src="https://theplanetd.com/images/Cities-in-Japan.jpg" alt="Slide 1" />
            </Slide>
            <Slide index={1}>
              <img src="https://wallpapercave.com/wp/wp4118244.jpg" alt="Slide 2" />
            </Slide>
            <Slide index={2}>
              <img src="" alt="" />
            </Slide>
          </Slider>
          <Dot slide={0}></Dot>
          <Dot slide={1}></Dot>
          <Dot slide={2}></Dot>
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