import './App.css';
import { useEffect, useState, useReducer } from 'react';

const formReducer = (state, event) => {
  if (event.reset) {
    return {
      salePrice: 0,
      downPayment: 0,
      mortgageDuration: 0,
      interestRate: 0,
      extraMonthlyPayment: 0
    }
  }

  return {
    ...state,
    [event.name]: (event.name === 'interestRate' && event.value.length <= 3) ? event.value : parseFloat(event.value)
  }
}

function App() {
  const [amortize, setAmortize] = useState([]);
  const [submitting, setSubmitting] = useState(false);
  const [formData, setFormData] = useReducer(formReducer, {});
  
  useEffect(() => {
    const apiRoute = process.env.REACT_APP_API_URL ? 
    `${process.env.REACT_APP_API_URL}/Amortize/CalculateAmortizationTable` : 
    `https://localhost:5001/Amortize/CalculateAmortizationTable`

    console.log(`apiRoute: ${apiRoute}`);

    fetch(apiRoute, 
      {
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        method: 'POST',
        body: JSON.stringify(formData)        
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
        setSubmitting(false);
        setAmortize(data);
      }
    ).catch(
      error => {
        console.log('ERROR: ' + error);
      }
    );
  }, [submitting]);

  const handleSubmit = event => {
    event.preventDefault();
    setSubmitting(true);
  };

  const handleChange = event => {
    setFormData({
      name: event.target.name,
      value: event.target.value
    })
  }

  return (
    <div className="App wrapper">
      {submitting && <div>Submitting form...</div>}
      <form onSubmit={handleSubmit}>
        <fieldset disabled={submitting} style={{ display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
          <div>
            <label>
              <p>Sale Price</p>
              <input name="salePrice" onChange={handleChange} value={formData.salePrice || ''} />
            </label>
            <label>
              <p>Down Payment</p>
              <input name="downPayment" onChange={handleChange} value={formData.downPayment || ''} />
            </label>
            <label>
              <p>Mortgage Duration (yrs)</p>
              <input name="mortgageDuration" onChange={handleChange} value={formData.mortgageDuration || ''} />
            </label>
            <label>
              <p>Interest Rate (annual)</p>
              <input name="interestRate" onChange={handleChange} value={formData.interestRate || ''} />
            </label>
          </div>
          <div>
            <label>
              <p>Home Insurance</p>
              <input name="homeInsurance" onChange={handleChange} value={formData.homeInsurance || ''} />
            </label>
            <label>
              <p>Property Tax</p>
              <input name="propertyTax" onChange={handleChange} value={formData.propertyTax || ''} />
            </label>
            <label>
              <p>Mortgage Insurance</p>
              <input name="mortgageInsurance" onChange={handleChange} value={formData.mortgageInsurance || ''} />
            </label>
            <label>
              <p>Extra Monthly Payment</p>
              <input name="extraMonthlyPayment" onChange={handleChange} value={formData.extraMonthlyPayment || ''} />
            </label>
          </div>
        </fieldset>

        <button type="submit" disabled={submitting}>Submit</button>
      </form>

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
