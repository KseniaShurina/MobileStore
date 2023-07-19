INSERT INTO "ProductTypes"("Id", "Name")
VALUES
(gen_random_uuid (), 'Phones'),
(gen_random_uuid (),'Watches'),
(gen_random_uuid (),'Laptops');

INSERT INTO "Products"("Id", "ProductTypeId", "Name", "Company", "Price", "Img")
VALUES
(gen_random_uuid (), 'cf5a119d-2a52-4389-b7db-d3338e494968', 'iPhone 14', 'Apple', 799.00, 'https://content2.onliner.by/catalog/device/header/18356816dfc5ab98f5ada5481b1d4c57.jpeg'),
(gen_random_uuid (), 'cf5a119d-2a52-4389-b7db-d3338e494968', 'iPhone 13', 'Apple', 599.99, 'https://content2.onliner.by/catalog/device/header/afb7e9aa0f63140ac19f34b3ac35a696.jpeg'),
(gen_random_uuid (), 'cf5a119d-2a52-4389-b7db-d3338e494968', 'iPhone 14 Pro', 'Apple', 899.00, 'https://content2.onliner.by/catalog/device/header/ad1fd08115cc6e1b4c289d580d79b406.jpeg'),
(gen_random_uuid (), 'cf5a119d-2a52-4389-b7db-d3338e494968', 'Xiaomi 13 Lite', 'Xiaomi', 465.00, 'https://content2.onliner.by/catalog/device/header/9c015621ef59f139b15b4cf3b34f9a29.jpeg'),
(gen_random_uuid (), 'cf5a119d-2a52-4389-b7db-d3338e494968', 'Redmi Note 12S', 'Xiaomi', 348.63, 'https://content2.onliner.by/catalog/device/header/f19149a21e1132ca7576d8f6a885ea1f.jpeg'),
(gen_random_uuid (), 'cf5a119d-2a52-4389-b7db-d3338e494968', 'Redmi Note 10 Pro', 'Xiaomi', 360.46, 'https://content2.onliner.by/catalog/device/header/050a6f0f727e91c7d6fb306dfce40a25.jpeg');