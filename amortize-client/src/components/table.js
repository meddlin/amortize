import './table.css';

function Table({ tableData }) {
    return (
        <div className="Table">
            {tableData && tableData.length > 0 ? 
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
                        {tableData.length > 0 ? 
                        tableData.map( 
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
                        ) : <tr></tr> }
                    </tbody>
                </table> : <h3>No data</h3> 
            }
        </div>
    );
};

export default Table;