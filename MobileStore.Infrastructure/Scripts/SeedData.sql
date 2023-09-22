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

INSERT INTO "Products"(
	"Id", "ProductTypeId", "Name", "Company", "Price", "Img")
	VALUES 
	(gen_random_uuid (), 
	 'edad1d74-08cf-42d1-a7f9-463a83bc8ee6', 
	 'Huawei Watch GT 3', 
	 'Huawei', 
	 300.95, 
	 'https://content2.onliner.by/catalog/device/header/4e3eb940c6638748b08c5239c22b5483.jpeg'),
	(gen_random_uuid (), 
	 'edad1d74-08cf-42d1-a7f9-463a83bc8ee6',
	 'Xiaomi Redmi Watch 3', 
	 'Xiaomi',
	 156.64, 
	 'https://content2.onliner.by/catalog/device/header/28cb76e4a66ee701897d5eac1a6c5118.jpeg');