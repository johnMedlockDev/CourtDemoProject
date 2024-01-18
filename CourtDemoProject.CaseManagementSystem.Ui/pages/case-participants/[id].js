import PropTypes from 'prop-types'
import axios from 'axios'

const CaseParticipantDetailPage = ({ caseParticipant }) => {
	return (
		<div>
			<h1>Case Participant Detail</h1>
			{caseParticipant
				? (
					<div>
						<p>Name: {caseParticipant.caseParticipantFirstName} {caseParticipant.caseParticipantMiddleName} {caseParticipant.caseParticipantLastName}</p>
						<p>Type: {caseParticipant.caseParticipantType}</p>
					</div>
				)
				: (
					<p>Case participant detail not found.</p>
				)}
		</div>
	)
}

export const getServerSideProps = async (context) => {
	const { id } = context.params

	try {
		const res = await axios.get(`http://api:8080/v1/CaseParticipants/${id}`)
		const caseParticipant = res.data // Adjust this according to the API response

		return {
			props: { caseParticipant }
		}
	} catch (error) {
		console.error('Error fetching case participant detail:', error)
		return {
			props: { caseParticipant: null }
		}
	}
}

CaseParticipantDetailPage.propTypes = {
	caseParticipant: PropTypes.shape({
		caseParticipantEntityId: PropTypes.string.isRequired,
		caseParticipantType: PropTypes.number.isRequired,
		caseParticipantFirstName: PropTypes.string.isRequired,
		caseParticipantLastName: PropTypes.string.isRequired,
		caseParticipantMiddleName: PropTypes.string
	})
}

export default CaseParticipantDetailPage
