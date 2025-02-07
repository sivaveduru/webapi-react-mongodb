import axios from "axios";

const API_URL = "http://localhost:5248/api/product";

export const getProducts = async () => {
  const response = await axios.get(API_URL);
  return response.data;
};

export const addProduct = async (product) => {
  await axios.post(API_URL, product);
};

export const deleteProduct = async (id) => {
  await axios.delete(`${API_URL}/${id}`);
};
