import { render, screen } from "@testing-library/react";
import App from "./App";

test("renders header and search button", () => {
  render(<App />);
  expect(screen.getByText(/order food and more/i)).toBeInTheDocument();
  expect(screen.getByText(/search/i)).toBeInTheDocument();
});
