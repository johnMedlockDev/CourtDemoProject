import PropTypes from 'prop-types'
import axios from 'axios'

const CaseDetailsPage = ({ caseDetails }) => {
	return (
		<div>
			<h1>Case Details</h1>
			<ul>
				{caseDetails.map((detail) => (
					<li key={detail.caseDetailId}>
						<p>Date: {new Date(detail.caseDetailEntryDateTime).toLocaleDateString()}</p>
						<p>Description: {detail.description}</p>
						<p>Docket Detail: {detail.docketDetail}</p>
						{detail.documentUri && <p>Document: <a href={detail.documentUri}>{detail.documentUri}</a></p>}
					</li>
				))}
			</ul>
		</div>
	)
}

export const getServerSideProps = async () => {
	// Fetch data from your API
	const res = await axios.get('http://api:8080/v1/CaseDetails')
	const caseDetails = res.data // Adjust this according to the API response

	return {
		props: { caseDetails }
	}
}

CaseDetailsPage.propTypes = {
	caseDetails: PropTypes.arrayOf(
		PropTypes.shape({
			caseDetailId: PropTypes.string.isRequired,
			caseDetailEntryDateTime: PropTypes.string.isRequired,
			description: PropTypes.string,
			docketDetail: PropTypes.string,
			documentUri: PropTypes.string
		})
	).isRequired
}

export default CaseDetailsPage
