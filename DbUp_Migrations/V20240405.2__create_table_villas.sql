CREATE TABLE villas (
    id UUID PRIMARY KEY NOT NULL,
    town_id UUID REFERENCES towns(id),
    name VARCHAR(100) NOT NULL,
    details VARCHAR(255),
    rate DOUBLE PRECISION,
    occupancy INT,
    image_url TEXT,
    amenity TEXT,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);