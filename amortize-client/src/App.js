import logo from './logo.svg';
import './App.css';
import { useEffect, useState } from 'react';

function App() {
  const [amortize, setAmortize] = useState([]);
  
  useEffect(() => {
    fetch("https://localhost:44353/Amortize/CalculateAmortizationTable", 
      {
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        method: 'POST',
        body: JSON.stringify({
          SalePrice: 530000,
          DownPayment: 53000,
          MortgageDuration: 30,
          InterestRate: 0.03625,
          ExtraMonthlyPayment: 200 + 500 + 900 + 1200
        })
      }
    ).then(
      response => {
        if (response.ok) {
          return response.json()
        }
        throw response;
      }
    ).then(
      data => {
        setAmortize(data);
      }
    ).catch(
      error => {
        console.log('ERROR: ' + error);
      }
    );
  }, []);

  return (
    <div className="App">
      <h2>Amortization Table</h2>
      <table>
        <thead>
          <tr>
            <th>Term</th>
            <th>Monthly Payment</th>
            <th>Principal</th>
            <th>Interest</th>
            <th>Remain. Pri.</th>
            <th>Extra Payment</th>
          </tr>
        </thead>
        <tbody>
        {
          amortize && amortize.length > 0 ? 
          amortize.map( 
            a => {
              return (
                  <tr key={a.term}>
                    <td>{a.term}</td>
                    <td>{a.monthlyPayment}</td>
                    <td>{a.principal}</td>
                    <td>{a.interest}</td>
                    <td>{a.remainingPrincipal}</td>
                    <td>{a.extraPayment}</td>
                  </tr>
              )
            } 
          ) : 'No data'
        }
        </tbody>
      </table>
    </div>
  );
}

export default App;
