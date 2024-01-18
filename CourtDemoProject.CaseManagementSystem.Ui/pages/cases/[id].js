import PropTypes from 'prop-types'
import axios from 'axios'

const CaseDetailPage = ({ caseItem }) => {
	return (
		<div>
			<h1>Case Detail</h1>
			{caseItem
				? (<div>
					<p>Court Name: {caseItem.courtName}</p>
					<p>Case Type: {caseItem.caseType}</p>
					{/* Display other details as needed */}
				</div>)
				: (<p>Case detail not found.</p>)}
		</div>
	)
}

export const getServerSideProps = async (context) => {
	const { id } = context.params

	try {
		const res = await axios.get(`http://api:8080/v1/Cases/${id}`)
		const caseItem = res.data // Adjust this according to the API response

		return {
			props: { caseItem }
		}
	} catch (error) {
		console.error('Error fetching case detail:', error)
		return {
			props: { caseItem: null }
		}
	}
}

CaseDetailPage.propTypes = {
	caseItem: PropTypes.shape({
		caseId: PropTypes.string.isRequired,
		courtName: PropTypes.string,
		caseType: PropTypes.number.isRequired
		// Include other properties as required
	})
}

export default CaseDetailPage
